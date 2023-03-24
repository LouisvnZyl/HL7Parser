namespace HL7MLLP_Test
{
    public class BaseHl7DataStructure : IBaseHL7DataStructure
    {
        public string SegmentName { get; set; } = string.Empty;

        public Dictionary<int, BaseHL7Field> Fields { get; set; } = new Dictionary<int, BaseHL7Field>();
    }
}
