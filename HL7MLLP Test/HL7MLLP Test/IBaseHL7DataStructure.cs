namespace HL7MLLP_Test
{
    public interface IBaseHL7DataStructure
    {
        public string SegmentName { get; set; }

        public Dictionary<int,BaseHL7Field> Fields { get; set; }
    }
}
