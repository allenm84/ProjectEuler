using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProjectEuler
{
  public partial class MainForm : Form
  {
    private BindingList<EulerProblem> problemBindingList = new BindingList<EulerProblem>();

    private List<EulerResult> results;
    private string resultsFilepath;
    private EulerTime[] times;

    public MainForm()
    {
      InitializeComponent();
      MinimumSize = Size;

      // subscribe to the load event
      Load += MainForm_Load;

      // initialize the times in order of magnitude
      times = new[]
      {
        EulerTime.Create(t => t.TotalMilliseconds, 1000, "ms"),
        EulerTime.Create(t => t.TotalSeconds, 60, "s"),
        EulerTime.Create(t => t.TotalMinutes, 60, "mins"),
        EulerTime.Create(t => t.TotalHours, 24, "hrs"),
        EulerTime.Create(t => t.TotalDays, double.MaxValue, "days")
      };
    }

    public void ShowError(string text)
    {
      MessageBox.Show(this, text, "Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void SetIsWorking(bool working)
    {
      progStatus.Visible = working;
      Cursor = working ? Cursors.WaitCursor : Cursors.Default;
      tableLayoutPanel1.Enabled = !working;
    }

    private void InitializeGrid()
    {
      gridMessages.DefaultCellStyle.Font = new Font("Lucida Console", 8.75f);
      gridMessages.RowTemplate.Height = 18;

      var eulerProblem = typeof(EulerProblem);
      var problems = (from type in typeof(EulerProblem).Assembly.GetTypes()
        where type.IsSubclassOf(eulerProblem) && !type.IsAbstract
        select Activator.CreateInstance(type) as EulerProblem)
        .OrderByDescending(p => p.Number);

      foreach (var problem in problems)
      {
        problem.Initialize(AddMessage);
        problemBindingList.Add(problem);
      }

      gridProblems.DataSource = problemBindingList;
      foreach (DataGridViewColumn column in gridProblems.Columns)
      {
        column.Visible = (column.DataPropertyName == "Display");
      }
    }

    private void UpdateUI()
    {
      var row = gridProblems.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
      var problem = (row != null) ? row.DataBoundItem as EulerProblem : null;
      var invalidProblem = (problem == null || gridProblems.SelectedRows.Count != 1);
      btnSolve.Enabled = !invalidProblem;
    }

    private void AddMessage(string message)
    {
      if (gridMessages.InvokeRequired)
      {
        gridMessages.BeginInvoke(new Action<string>(AddMessage), message);
      }
      else
      {
        var row = gridMessages.Rows.Add(message);
        gridMessages.CurrentCell = gridMessages[0, row];
      }
    }

    private async void DoLoad()
    {
      SetIsWorking(true);

      await Task.Run(() => DoLoadWork());
      DoLoadRunWorkerCompleted();

      SetIsWorking(false);
    }

    private void DoLoadWork()
    {
      // is there a solution file for us to read from
      resultsFilepath = Path.Combine(Application.StartupPath, "solutions.xml");
      if (File.Exists(resultsFilepath))
      {
        // there is, let's read from it
        using (var stream = File.OpenRead(resultsFilepath))
        {
          results = EulerResult.ListSerializer.ReadObject(stream) as List<EulerResult>;
        }
      }

      // initialize the solutions if there are none
      if (results == null)
      {
        results = new List<EulerResult>();
      }
    }

    private void DoLoadRunWorkerCompleted()
    {
      // initialize the grid
      InitializeGrid();

      // update the ui for good measure
      UpdateUI();
    }

    private async void DoSave()
    {
      SetIsWorking(true);
      await Task.Run(() => DoSaveWork());
      SetIsWorking(false);
    }

    private void DoSaveWork()
    {
      var settings = new XmlWriterSettings {Indent = true};
      using (var writer = XmlWriter.Create(resultsFilepath, settings))
      {
        EulerResult.ListSerializer.WriteObject(writer, results);
      }
    }

    private async void DoSolve()
    {
      SetIsWorking(true);

      var problem = gridProblems.SelectedRows[0].DataBoundItem;
      var result = await Task.Run(() => DoSolveWork(problem));
      DoSolveRunWorkerCompleted(result);

      SetIsWorking(false);
    }

    private EulerResult DoSolveWork(object argument)
    {
      // retrieve the problem
      var problem = argument as EulerProblem;

      // write a message
      AddMessage(string.Format("Starting {0}", problem.Display));

      // create a euler result
      var result = new EulerResult();
      result.Problem = problem.Number;

      // solve the problem
      var stopwatch = Stopwatch.StartNew();
      result.Result = problem.Solve();
      stopwatch.Stop();
      result.Timespan = stopwatch.Elapsed;

      // return the result!
      return result;
    }

    private void DoSolveRunWorkerCompleted(EulerResult result)
    {
      result.Solved = DateTime.Now;
      results.Add(result);

      var span = result.Timespan;
      foreach (var time in times)
      {
        var value = time.Time(span);
        if (value <= time.Maximum)
        {
          AddMessage(string.Format("Solved in {0} {1}", value, time.Units));
          AddMessage("========================");
          break;
        }
      }

      txtResult.Text = result.Result.ToString();
      DoSave();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      DoLoad();
    }

    private void gridProblems_SelectionChanged(object sender, EventArgs e)
    {
      UpdateUI();
    }

    private void btnSolve_Click(object sender, EventArgs e)
    {
      DoSolve();
    }

    private void gridProblems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var row = gridProblems.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
      var problem = (row != null) ? row.DataBoundItem as EulerProblem : null;

      var invalidProblem = (problem == null || gridProblems.SelectedRows.Count != 1);
      if (!invalidProblem)
      {
        Process.Start(problem.Url);
      }
    }
  }
}