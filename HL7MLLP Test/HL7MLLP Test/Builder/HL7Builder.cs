namespace HL7MLLP_Test.Builder
{
    public static class HL7Builder
    {
        public static BaseHl7DataStructure WithSegmentName(this BaseHl7DataStructure hl7Data, string segmentName)
        {
            hl7Data.SegmentName = segmentName;
            return hl7Data;
        }

        public static BaseHl7DataStructure WithFields(this BaseHl7DataStructure hl7Data, Func<Dictionary<int, BaseHL7Field>, Dictionary<int, BaseHL7Field>> segmentConfiguration)
        {
            segmentConfiguration(hl7Data.Fields);
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
