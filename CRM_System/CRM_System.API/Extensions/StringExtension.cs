namespace CRM_System.API.Extensions
{
    public static class StringExtension
    {
        public static string Mask(this string originalData)
        {
            if (originalData.Length<4)
            {

            }
            string maskedData = originalData.Remove(0, 2);
            maskedData = maskedData.Remove(maskedData.Length-2, 2);

            return $"**{maskedData}**";
        }
    }
}
