namespace HL7MLLP_Test
{
    public class Hl7Segment
    {
        public string SegmentName { get; set; } = string.Empty;

        public Dictionary<int, BaseHL7Field> Fields { get; set; } = new Dictionary<int, BaseHL7Field>();
    }
}
