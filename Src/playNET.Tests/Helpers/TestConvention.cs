using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fixie;
using Fixie.Conventions;

namespace playNET.Tests.Helpers
{
    public class TestConvention : Convention
    {
        public TestConvention()
        {
            Classes.NameEndsWith("Tests");
            Methods.Where(method => method.IsVoid());
            Parameters(FromInputAttributes);
        }

        private static IEnumerable<object[]> FromInputAttributes(MethodInfo method)
        {
            return method.GetCustomAttributes<InputAttribute>(true).Select(input => input.Parameters);
        }
    }
}