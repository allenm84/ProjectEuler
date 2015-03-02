using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ProjectEuler
{
  [DataContract(Name = "EulerResult", Namespace = "http://www.projecteuler.net/solutions")]
  public class EulerResult : IExtensibleDataObject
  {
    static Lazy<DataContractSerializer> dcs = new Lazy<DataContractSerializer>(() =>
      new DataContractSerializer(typeof(EulerResult)), true);
    public static DataContractSerializer Serializer { get { return dcs.Value; } }

    static Lazy<DataContractSerializer> listdcs = new Lazy<DataContractSerializer>(() =>
      new DataContractSerializer(typeof(List<EulerResult>)), true);
    public static DataContractSerializer ListSerializer { get { return listdcs.Value; } }

    public ExtensionDataObject ExtensionData { get; set; }

    [DataMember]
    public TimeSpan Timespan { get; set; }
    [DataMember]
    public object Result { get; set; }
    [DataMember]
    public int Problem { get; set; }
    [DataMember]
    public DateTime Solved { get; set; }
  }
}
