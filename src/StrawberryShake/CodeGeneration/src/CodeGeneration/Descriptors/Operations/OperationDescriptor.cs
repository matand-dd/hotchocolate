using System;
using System.Collections.Generic;

namespace StrawberryShake.CodeGeneration
{
    /// <summary>
    /// Describes a GraphQl operation
    /// </summary>
    public abstract class OperationDescriptor : ICodeDescriptor
    {
        /// <summary>
        /// The name of the operation
        /// </summary>
        public abstract string Name { get; }

        public string Namespace { get; }

        /// <summary>
        /// The type the operation returns
        /// </summary>
        public ITypeDescriptor ResultTypeReference { get; }

        public string BodyString { get; }

        /// <summary>
        /// The arguments the operation takes.
        /// </summary>
        public IReadOnlyList<NamedTypeReferenceDescriptor> Arguments { get; }

        public OperationDescriptor(
            ITypeDescriptor resultTypeReference,
            string @namespace,
            IReadOnlyList<NamedTypeReferenceDescriptor> arguments,
            string bodyString)
        {
            ResultTypeReference = resultTypeReference;
            Arguments = arguments;
            BodyString = bodyString;
            Namespace = @namespace;
        }
    }
}
