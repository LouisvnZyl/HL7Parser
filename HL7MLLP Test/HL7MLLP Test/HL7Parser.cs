using HL7.Dotnetcore;
using HL7MLLP_Test.Builder;

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
OBX | @@SetId | ED | 02585 ^ SummaryOfCare || ^Application ^ PDF ^ Base64 ^ @@Base64Pdf1 ||||||";

            Message message = new Message(adtMessageToValidateAgainst);

            var isParsedd = message.ParseMessage(true);

            List<Segment> segments = message.Segments();

            message.SetValue("OBX.5.5", "Bloopy");

            var obxSegmentFields = message.GetValue("OBX.5.5");

            var messRes = message.SerializeMessage(false);

            Message testingMessage = new Message(string.Empty);

            BaseHl7DataStructure testingStructure = new BaseHl7DataStructure();

            testingStructure.WithSegment("MHS", segment => segment.WithFields(fields => fields.WithField(2, component => component.WithComponent(1, "Seg 1 Field 1 Comp 1")
                                                                                                                                  .WithComponent(3, "Seg 1 Field 1 Comp 3"))
                                                                                              .WithField(4, component => component.WithComponent(2, "Seg 1 Field 4 Comp 2")
                                                                                                                                  .WithComponent(6, "Seg 1 Field 4 Comp 6"))))
                            .WithSegment("OBX", segment => segment.WithFields(fields => fields.WithField(1, component => component.WithComponent(1, "Seg 2 Field 1 Comp 1"))
                                                                                              .WithField(3, component => component.WithComponent(1, "Seg 2 Field 3 Comp 1"))
                                                                                              .WithField(5, component => component.WithComponent(1, "Seg 2 Field 5 Comp 1"))
                                                                                              .WithField(7, component => component.WithComponent(1, "Seg 2 Field 7 Comp 1"))
                                                                                              .WithField(11, component => component.WithComponent(1, "Seg 2 Field 11 Comp 1"))));

            CreateHl7Document(testingStructure, testingMessage);

            var newTestMessage = testingMessage.SerializeMessage(false);
        }

        public void CreateHl7Document(IBaseHL7DataStructure hl7DataStructure, Message message)
        {
            HL7Encoding hL7Encoding = new HL7Encoding();

            foreach (var segment in hl7DataStructure.Segments)
            {
                Segment newSegment = new Segment(segment.SegmentName, hL7Encoding);

                foreach (var item in segment.Fields)
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
}
