using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ProjectEuler
{
  [DataContract(Name = "EulerResult", Namespace = "http://www.projecteuler.net/solutions")]
  public class EulerResult : IExtensibleDataObject
  {
    private static Lazy<DataContractSerializer> dcs = new Lazy<DataContractSerializer>(() =>
      new DataContractSerializer(typeof(EulerResult)), true);

    private static Lazy<DataContractSerializer> listdcs = new Lazy<DataContractSerializer>(() =>
      new DataContractSerializer(typeof(List<EulerResult>)), true);

    public static DataContractSerializer Serializer
    {
      get { return dcs.Value; }
    }

    public static DataContractSerializer ListSerializer
    {
      get { return listdcs.Value; }
    }

    [DataMember]
    public TimeSpan Timespan { get; set; }
    [DataMember]
    public object Result { get; set; }
    [DataMember]
    public int Problem { get; set; }
    [DataMember]
    public DateTime Solved { get; set; }

    #region IExtensibleDataObject Members

    public ExtensionDataObject ExtensionData { get; set; }

    #endregion
  }
}