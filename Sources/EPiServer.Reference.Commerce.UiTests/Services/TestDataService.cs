using System;

namespace EPiServer.Reference.Commerce.UiTests.Services
{
    public static class TestDataService
    {
        public static string FirstName => "John";
        public static string LastName => "Doe";
        public static string Street => "Hornsgatan 123";
        public static string ZipCode => "12345";
        public static string City => "Stockholm";
        public static string PhoneNumber => "0706050403";
        public static string SwedishPhoneNumber => "+46706050403";
        public static string PersonalNumber => "19800101-8921";
        public static string PersonalNumberShort => "800101-8921";
        public static string Email => "someone@somemail.com";


        public static string ClearingNumber = "1234";
        public static string AccountNumber => "1234567890";
        public static string CreditCardNumber => "4925000000000004";
        public static string CreditCardCvc => "210";
        public static string CreditCardExpiratioDate => DateTime.Now.AddMonths(3).AddYears(1).ToString("MMyy");
        public static string SwishPhoneNumber => "0739000001";

        public static string ManagerUsername = "admin@example.com";
        public static string ManagerPassword = "store";


        public static string LoremIpsum => "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent convallis facilisis neque ut scelerisque. Morbi arcu purus, gravida sed velit nec, interdum egestas ante. Pellentesque dapibus nisl ultrices dolor placerat, eu lobortis mauris elementum. Curabitur placerat ante est. Fusce et massa est. Etiam quis lacus justo. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Phasellus nulla enim, ornare in facilisis quis, ornare nec erat. Nullam sit amet mi augue. Proin dignissim risus urna, sed pulvinar turpis sollicitudin quis. Proin pretium lacinia ullamcorper.";
        public static string Description(int length = 30) => LoremIpsum.Substring(0, length);

        #region Varvare

        public static string LoginEmail => "test.okta@authority.se";
        public static string LoginPassword => "Superhemligt#3";

        #endregion Varvare
    }
}
