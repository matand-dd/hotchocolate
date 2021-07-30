using System;
using System.Collections.Generic;
using HotChocolate.Language;
using HotChocolate.Properties;
using HotChocolate.Types.Descriptors.Definitions;

#nullable enable

namespace HotChocolate.Types
{
    /// <summary>
    /// A GraphQL Input Object defines a set of input fields; the input fields are either scalars,
    /// enums, or other input objects. This allows arguments to accept arbitrarily complex structs.
    ///
    /// In this example, an Input Object called Point2D describes x and y inputs:
    ///
    /// <code>
    /// input Point2D {
    ///   x: Float
    ///   y: Float
    /// }
    /// </code>
    /// </summary>
    public partial class InputObjectType
        : NamedTypeBase<InputObjectTypeDefinition>
        , IInputObjectType
    {
        /// <inheritdoc />
        public override TypeKind Kind => TypeKind.InputObject;

        /// <summary>
        /// Gets the GraphQL syntax representation of this type
        /// if it was provided during initialization.
        /// </summary>
        public InputObjectTypeDefinitionNode? SyntaxNode { get; private set; }

        /// <summary>
        /// Gets the fields of this type.
        /// </summary>
        public FieldCollection<InputField> Fields { get; private set; } = default!;

        IFieldCollection<IInputField> IInputObjectType.Fields => Fields;

        /// <inheritdoc />
        public virtual bool IsInstanceOfType(IValueNode literal)
        {
            if (literal is null)
            {
                throw new ArgumentNullException(nameof(literal));
            }

            return literal is ObjectValueNode or NullValueNode;
        }

        /// <inheritdoc />
        public virtual bool IsInstanceOfType(object? value)
        {
            return value is null || RuntimeType.IsInstanceOfType(value);
        }
    }
}
