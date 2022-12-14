namespace CRM_System.API.Extensions
{
    public static class StringExtension
    {
        public static string MaskEmail(this string originalData)
        {
            int index = originalData.IndexOf('@');
            string output = originalData;
            if (index > 3)
            {
                output = originalData.Substring(0, 2) + new string('*', index - 2) + originalData.Substring(index);
            }
            return output;

        }

        public static string MaskNumber(this string originalData)
        {
            string firstFourNumbers = originalData.Substring(0, 4);
            string theLastTwoNumbers = originalData.Substring(9, 2);
            string maskedNumber = firstFourNumbers.PadRight(9, '*');
            maskedNumber += theLastTwoNumbers;
            return maskedNumber;
        }

        public static string MaskPassport(this string originalData)//принимать два параметра
        {
            string firstFourNumbers = originalData.Substring(0, 2);
            string theLastTwoNumbers = originalData.Substring(8, 2);
            string maskedNumber = firstFourNumbers.PadRight(8, '*');
            maskedNumber += theLastTwoNumbers;
            return maskedNumber;
        }
        public static string MaskTheLastFive(this string originalData)
        {
            string maskedData = originalData.Remove(originalData.Length - 5, 5);
            return $"{maskedData} *****";
        }
    }
}
