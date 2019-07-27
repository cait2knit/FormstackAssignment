using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormstackAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new FormTests();
            test.GetAllFormsWithoutAuthToken();
            test.GetAllFormsWithAuthToken();
            test.GetSpecificKnownForm();
            test.GetFormByIdRange();
            test.CopyFormById();
            test.CopyFormIdRange();
            test.DeleteCopiedForm();
            test.DeleteFormIdRange();

        int totalTestsInt = test.success + test.fail;
        decimal totalTests = Convert.ToDecimal(totalTestsInt);
        decimal successRate = Math.Round(((test.success / totalTests) * 100), 1);
        decimal failureRate = Math.Round(((test.fail / totalTests) * 100), 1);


        Console.WriteLine("Total number of tests: " + totalTests);
        Console.WriteLine("Success Rate= " + successRate + "%");
        Console.WriteLine("Failure Rate= " + failureRate + "%");

        Console.ReadKey();
        }
    }
}
