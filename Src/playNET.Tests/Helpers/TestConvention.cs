using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fixie.Conventions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Ploeh.AutoFixture.Kernel;
using ReflectionExtensions = Fixie.ReflectionExtensions;

namespace playNET.Tests.Helpers
{
    public class TestConvention : Convention
    {
        private readonly ISpecimenContext specimen = new SpecimenContext(new Fixture().Customize(new AutoFakeItEasyCustomization()));

        public TestConvention()
        {
            Classes.NameEndsWith("Tests");
            Methods.Where(method => ReflectionExtensions.IsVoid(method));
            Parameters(ForMethod);
        }

        private IEnumerable<object[]> ForMethod(MethodInfo method)
        {
            var inputs = method.GetCustomAttributes<InputAttribute>(true);
            if (inputs.Any())
                return inputs.Select(input => input.Parameters);

            return AutofixtureParametersFor(method);
        }

        private IEnumerable<object[]> AutofixtureParametersFor(MethodInfo method)
        {
            var parameterTypes = method.GetParameters().Select(x => x.ParameterType);
            var parameterValues = parameterTypes.Select(t => specimen.Resolve(t)).ToArray();
            return new[] {parameterValues};
        }
    }
}