using HL7.Dotnetcore;

namespace HL7MLLP_Test
{
    public class HL7Parser
    {
        public HL7Parser()
        {

        }

        public void ParseHl7()
        {
            // This seems to be some form of validator
            const string adtMessageToValidateAgainst =
                @"MSH|@@FieldSeparator|@@SourceAppId^@@SourceAppName|@@SourceEnviroment^@@SendingFacilitySapKey^@@SendingFacility|@@RecievingApplication|@@RecievingFacility|@@DateTimeOfMessage||MDM^T02|@@Guid|P|2.3|||NE|
EVN | T02 | @@RecordedDateTime |||| @@EventOccured |
            PID || @@PatientExternalId | @@PatientInternalId | @@PatientIdAlternate | @@PatientLastName ^ @@PatientFirstName ^ ^^@@PatientTitle || @@DateOfBirth | @@Gender ||||| @@MobileNumber ^ @@AlternativeNumber ^ @@Email |||||| @@SsnNumberPatient ||||||||||| @@PatientDeathIndicator |
PV1 || @@PatientClass | @@SendingFacilitySapKey ^ ^^^^^^@@AssignedPatientLocation | @@AdmissionType | @@PreAdmitNumber || @@AttendingDoctorReference ^ @@AttendingDoctorLastName ^ @@AttendingDoctorFirstName | @@ReferringDoctorReference ^ @@ReferringDoctorLastName ^ @@ReferringDoctorFirstName | @@ConsultingDoctorReference ^ @@ConsultingDoctorLastName ^ @@ConsultingDoctorFirstName |||||||| @@AdmittingDoctorReference ^ @@AdmittingDoctorLastName ^ @@AdmittingDoctorFirstName ||||||||||||||||||||||||||| @@AdmissionDateTime | @@DischargeDateTime |
TXA || @@DocumentType ^ Summary Of Care|| @@ActivityDateTime ||||||||||||||||
OBX | @@SetId | ED | 02585 ^ SummaryOfCare || ^Application ^ PDF ^ Base64 ^ @@Base64Pdf1 ||||||
OBX | @@SetId | ED | 02585 ^ SummaryOfCare || ^Application ^ PDF ^ Base64 ^ @@Base64Pdf2 ||||||
";

            Message message = new Message(adtMessageToValidateAgainst);

            var isParsedd = message.ParseMessage(true);

            List<Segment> segments = message.Segments();

            message.SetValue("OBX.5.5", "Bloopy");

            var obxSegmentFields = message.GetValue("OBX(2).5.5");

            var messRes = message.SerializeMessage(false);

            Message testingMessage = new Message(string.Empty);

            BaseHl7DataStructure structure = new BaseHl7DataStructure
            {
                SegmentName = "MSH",
                Fields = new Dictionary<int, BaseHL7Field>
                {
                    {
                        2,
                        new BaseHL7Field
                        {
                            Components = new Dictionary<int, string>
                            {
                                {1, "Characters_Here" }
                            }
                        }
                    },
                    {
                        3,
                        new BaseHL7Field
                        {
                            Components = new Dictionary<int, string>
                            {
                                {1, "SENDING_APPLICATION" },
                                {3, "SENDING_APPLICATION_3" }
                            }
                        }
                    },
                    {
                        6,
                        new BaseHL7Field
                        {
                            Components = new Dictionary<int, string>
                            {
                                {1, "adasdasdasd" },
                                {4, "asdasd" }
                            }
                        }
                    },
                }
            };

            CreateHl7Document(structure, testingMessage);

            var newTestMessage = testingMessage.SerializeMessage(false);
        }

        public void CreateNewSegment(Message message)
        {
            HL7Encoding hL7Encoding = new HL7Encoding();

            Segment newSegment = new Segment("Test", hL7Encoding);

            Field field1 = new Field("Test1", hL7Encoding);
            Field field5 = new Field("Test5", hL7Encoding);

            Component com1 = new Component("Test.5.2", hL7Encoding);

            field5.AddNewComponent(com1, 4);

            newSegment.AddNewField(field1);
            newSegment.AddNewField(field5, 10);

            bool success = message.AddNewSegment(newSegment);

            if (!success)
            {
                throw new Exception();
            }
        }

        public void CreateHl7Document(IBaseHL7DataStructure hl7DataStructure, Message message)
        {
            HL7Encoding hL7Encoding = new HL7Encoding();

            Segment newSegment = new Segment(hl7DataStructure.SegmentName, hL7Encoding);

            foreach (var item in hl7DataStructure.Fields)
            {
                Field segmentField = new Field(string.Empty, hL7Encoding);

                foreach (var component in item.Value.Components)
                {
                    Component fieldComponent = new Component(component.Value, hL7Encoding);

                    segmentField.AddNewComponent(fieldComponent, component.Key);
                }

                newSegment.AddNewField(segmentField, item.Key);
            }

            bool success = message.AddNewSegment(newSegment);

            if (!success)
            {
                throw new Exception();
            }
        }
    }
}
