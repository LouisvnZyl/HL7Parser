namespace HL7MLLP_Test
{
    public class BaseHl7DataStructure : IBaseHL7DataStructure
    {
        public List<Hl7Segment> Segments { get; set; } = new List<Hl7Segment>();
    }
}
