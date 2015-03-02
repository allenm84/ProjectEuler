﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Data;

namespace ProjectEuler
{
  public class Problem138 : EulerProblem
  {
    const double DblSqrt5 = 2.23606797749978969640917366873127623544061835961152572427089724541052092563780489941441440837878227496950817615077378350425326772444707386358636012153345270886677817319187916581127664532263985658053576135041753378500342339241406444208643253909725259262722887629951740244068161177590890949849237139072972889848208864154268989409913169357701974867888442508975413295618317692149997742480153043411503595766833251249881517813940800056242085524354223555610630634282023409333198293395974635227120134174961420263590473788550438968706113566004575713995659556695691756457822195250006053923123400500928676487552972205676625366607448585350526233067849463342224231763727702663240768010444331582573350589309813622634319868647194698997018081895242644596203452214119223291259819632581110417049580704812040345599494350685555185557251238864165501026243631257102444961878942468290340447471611545572320173767659046091852957560357798439805415538077906439363972302875606299948221385217734859245351512104634555504070722787242153477875291121212118433178933519103800801111817900459061884624964710424424830888012940681131469595327944789899893169157746079246180750067987712420484738050277360829155991396244891494356068346252906440832794464268088898974604630835353787504206137475760688340187908819255911797357446419024853787114619409019191368803511039763843604128105811037869895185201469704564202176389289088444637782638589379244004602887540539846015606170522361509038577541004219368498725427185037521555769331672300477826986666244621067846427248638527457821341006798564530527112418059597284945519545131017230975087149652943628290254001204778032415546448998870617799819003360656224388640963928775351726629597143822795630795614952301544423501653891727864091304197939711135628213936745768117492206756210888781887367167162762262337987711153950968298289068301825908140100389550972326150845283458789360734639611723667836657198260792144028911900899558424152249571291832321674118997572013940378819772801528872341866834541838286730027431532022960762861252476102864234696302011180269122023601581012762843054186171761857514069010156162909176398126722596559628234906785462416185794558444265961285893756485497480349011081355751416647462195183023552595688656949581635303619557453683223526500772242258287366875340470074223266145173976651742067264447621961802422039798353682983502466268030546768767446900186957209958589198316440251620919646185105744248274087229820410943710992236175285315302212109176295120886356959716907946257260325089752229704043412880822332153390119551566514079022175646165421295787804223138207855367690772666643131659319546206872064645091487274408248812817765347516867907359186246442687464199149977893991312947201459199967825762063948526250359428286402462255910378955634538283178235598391296251160036910131265905719718200181724360595512757851998329989285638604458710469334951865390330842804218272603638944541578024417457472341469729999631251094562274695974331390549780162887681065496756275649338348884592698294163140147050914141795453509386876452390937230662419067158476029218547020420238380436721350194617915057915493628459086788770986310679260761458338351692202921990110129607358608294473144079720147101521804634625003226409687167296354096963621983204885046543344380378669192757217575057403478718606026718022474204783425318094052698805661533753487277302654212560646348138634668964687129063701162706217099466701519933557424898116727350826578172481264912790714425048522340556057312086469885674603451148811674556535992063478728026575255402487359662289287389534106254498482094334002764956625731301298686836078008203561067901175449173311510458783164794168354596674564623051385218599188448000112125335734871584794490816963530394687253053;
    const double DblOneFifth = 1.0 / 5.0;

    public override int Number { get { return 138; } }

    public override object Solve()
    {
      // We are basically solving this equation:
      // h^2 + (b/2)^2 = L^2
      //
      // However, h is b +/- 1, so we're solving two equations:
      // (b+1)^2 + (b/2)^2 = L^2
      // (b-1)^2 + (b/2)^2 = L^2
      //
      // Plugging these into wolfram alpha generated two equations for each of the
      // two above (so 4 total). They involved getting the b value for a particular
      // n value. I create 4 functions for each equation and noticed that first equation
      // generated negative values while the second one didn't. So, I only used the second
      // equation (since b > 0). From there, I was able to get 12 b values. Each of these is
      // valid (since they solve the first equation) so it was simply a matter of finding L.

      // we only care about the first 12 values. Since we'll be using two different functions
      // to get the n values and 12/2 is 6, we go from 1 to 6.
      var ns = new int[] { 1, 2, 3, 4, 5, 6 };

      // generate the valid b values
      var validBValues = new List<ulong>();
      foreach (var n in ns)
      {
        validBValues.Add((ulong)CalcBM_x2_dbl(n));
        validBValues.Add((ulong)CalcBP_x2_dbl(n));
      }
      validBValues.Sort();

      // sum up the L values
      BigInteger sum = 0;
      foreach (var v in validBValues)
      {
        BigInteger asqrd = v >> 1;
        asqrd *= asqrd;

        BigInteger[] bs = new BigInteger[] { v + 1, v - 1 };
        foreach (var b in bs)
        {
          BigInteger bsqrd = b;
          bsqrd *= bsqrd;

          BigInteger length = asqrd + bsqrd;
          BigInteger c = MathHelper.Sqrt(length);

          BigInteger csqrd = (c * c);
          if (csqrd == length)
          {
            sum += c;
          }
        }
      }

      // return the sum
      return sum;
    }

    private static double CalcBP_x1_dbl(int n)
    {
      double a = 2 * Math.Pow(9 - (4 * DblSqrt5), 2 * n);
      double b = DblSqrt5 * Math.Pow(9 - (4 * DblSqrt5), 2 * n);
      double c = 2 * Math.Pow(9 + (4 * DblSqrt5), 2 * n);
      double d = DblSqrt5 * Math.Pow(9 + (4 * DblSqrt5), 2 * n);
      return DblOneFifth * (a + b + c - d - 4);
    }

    private static double CalcBP_x2_dbl(int n)
    {
      double a = 2 * Math.Pow(9 - (4 * DblSqrt5), 2 * n);
      double b = DblSqrt5 * Math.Pow(9 - (4 * DblSqrt5), 2 * n);
      double c = 2 * Math.Pow(9 + (4 * DblSqrt5), 2 * n);
      double d = DblSqrt5 * Math.Pow(9 + (4 * DblSqrt5), 2 * n);
      return DblOneFifth * (a - b + c + d - 4);
    }

    private static double CalcBM_x1_dbl(int n)
    {
      double a = -2 * Math.Pow(9 - (4 * DblSqrt5), 2 * n);
      double b = DblSqrt5 * Math.Pow(9 - (4 * DblSqrt5), 2 * n);
      double c = 2 * Math.Pow(9 + (4 * DblSqrt5), 2 * n);
      double d = DblSqrt5 * Math.Pow(9 + (4 * DblSqrt5), 2 * n);
      return DblOneFifth * (a + b - c - d + 4);
    }

    private static double CalcBM_x2_dbl(int n)
    {
      double a = -2 * Math.Pow(9 - (4 * DblSqrt5), 2 * n);
      double b = DblSqrt5 * Math.Pow(9 - (4 * DblSqrt5), 2 * n);
      double c = 2 * Math.Pow(9 + (4 * DblSqrt5), 2 * n);
      double d = DblSqrt5 * Math.Pow(9 + (4 * DblSqrt5), 2 * n);
      return DblOneFifth * (a - b - c + d + 4);
    }
  }
}
