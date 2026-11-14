namespace MechanicShopPOO
{
    public static class InputHelper
    {
        public static int ReadNumber(string message)
        {
            Console.Write(message);
            return Convert.ToInt32(Console.ReadLine());
        }

        public static string ReadText(string message)
        {
            string text = string.Empty;

            while (text == string.Empty)
            {
                Console.Write(message);
                text = Console.ReadLine();

                if (text == string.Empty)
                {
                    Console.WriteLine("This field cannot be empty.");
                }
            }

            return text;
        }

        public static bool AskYesNo(string message)
        {
            string answer = string.Empty;

            while (answer != "yes" && answer != "no")
            {
                Console.Write(message);
                answer = Console.ReadLine().ToLower();

                if (answer != "yes" && answer != "no")
                {
                    Console.WriteLine("Please type 'yes' or 'no'.");
                }
            }

            return answer == "yes";
        }
    }
}
