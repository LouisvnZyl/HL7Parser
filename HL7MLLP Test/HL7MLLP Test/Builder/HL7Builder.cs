namespace HL7MLLP_Test.Builder
{
    public static class HL7Builder
    {
        public static BaseHl7DataStructure WithSegment(this BaseHl7DataStructure hl7Data, string segmentName, Func<Hl7Segment, Hl7Segment> segmentConfiguration)
        {
            Hl7Segment segment = new Hl7Segment();
            segment.SegmentName = segmentName;
            segmentConfiguration(segment);
            hl7Data.Segments.Add(segment);
            return hl7Data;
        }

        public static Hl7Segment WithFields(this Hl7Segment hl7Data, Func<Dictionary<int, BaseHL7Field>, Dictionary<int, BaseHL7Field>> fieldConfiguration)
        {
            fieldConfiguration(hl7Data.Fields);
            return hl7Data;
        }

        public static Dictionary<int, BaseHL7Field> WithField(this Dictionary<int, BaseHL7Field> hl7Fields, int fieldIndex, Func<Dictionary<int, string>, Dictionary<int, string>> componentConfiguration)
        {
            BaseHL7Field field = new BaseHL7Field();
            componentConfiguration(field.Components);

            hl7Fields.Add(fieldIndex, field);
            return hl7Fields;
        }

        public static Dictionary<int, string> WithComponent(this Dictionary<int, string> components, int componentIndex, string componentValue)
        {
            components.Add(componentIndex, componentValue);
            return components;
        }
    }
}
