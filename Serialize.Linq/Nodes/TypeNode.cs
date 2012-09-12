﻿using System;
using System.Runtime.Serialization;
using Serialize.Linq.Interfaces;

namespace Serialize.Linq.Nodes
{
    [DataContract]
    public class TypeNode : Node
    {
        public TypeNode(IExpressionNodeFactory factory, Type type)
            : base(factory)
        {
            if (type != null)
                this.Name = this.Factory.UseAssemblyQualifiedName ? type.AssemblyQualifiedName : type.FullName;
        }

        [DataMember]
        public string Name { get; set; }

        public Type ToType()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                return null;
            var type = Type.GetType(this.Name);
            if (type == null)
                throw new SerializationException(string.Format("Failed to serialize '{0}' to a type object.", this.Name));
            return type;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }    
}