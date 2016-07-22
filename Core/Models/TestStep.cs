using System;
using System.Xml.Serialization;
using Common.Enums;

namespace Core.Models
{
    [Serializable]
    [XmlType("teststep")]
    public class TestStep
    {
        [XmlElement("stepId")]
        public string StepId { get; set; }

        [XmlElement("orderId")]
        public int OrderId { get; set; }

        [XmlElement("step")]
        public string Step { get; set; }

        [XmlElement("data")]
        public string Data { get; set; }

        [XmlElement("result")]
        public string Result { get; set; }

        [XmlElement("stepStatus", typeof(StepStatus))]
        public StepStatus StepStatus { get; set; }

        [XmlElement("stepComment")]
        public string StepComment { get; set; }
    }
}
