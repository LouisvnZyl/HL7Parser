namespace HL7MLLP_Test.Builder
{
    public static class HL7Builder
    {
        public static BaseHl7DataStructure WithSegmentName(this BaseHl7DataStructure hl7Data, string segmentName)
        {
            hl7Data.SegmentName = segmentName;
            return hl7Data;
        }

        public static BaseHl7DataStructure WithField(this BaseHl7DataStructure hl7Data, int index, Func<Dictionary<int, BaseHL7Field>, Dictionary<int, BaseHL7Field>> segmentConfiguration)
        {
            segmentConfiguration(hl7Data.Fields);
            return hl7Data;
        }

        public static BaseHL7Field WithFieldSegment(this BaseHL7Field hl7Field, int index, Func<Dictionary<int, string>, Dictionary<int, string>> componentConfiguration)
        {
            componentConfiguration(hl7Field.Components);
            return hl7Field;
        }
    }
}
