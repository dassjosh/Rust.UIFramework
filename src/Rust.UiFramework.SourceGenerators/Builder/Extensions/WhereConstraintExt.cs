using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Builder.Enums;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;

namespace Rust.UiFramework.SourceGenerators.Builder.Extensions;

public static class WhereConstraintExt
{
    extension<T>(T builder) where T : IWhere
    {
        public T Class()
        {
            builder.Constraints ??= [];
            builder.Constraints.Add(WhereConstraint.Class);
            return builder;
        }
    
        public T Struct()
        {
            builder.Constraints ??= [];
            builder.Constraints.Add(WhereConstraint.Struct);
            return builder;
        }
    
        public T Unmanaged()
        {
            builder.Constraints ??= [];
            builder.Constraints.Add(WhereConstraint.Unmanaged);
            return builder;
        }
    
        public T New()
        {
            builder.Constraints ??= [];
            builder.Constraints.Add(WhereConstraint.New);
            return builder;
        }
    
        public T NotNull()
        {
            builder.Constraints ??= [];
            builder.Constraints.Add(WhereConstraint.NotNull);
            return builder;
        }
    
        public T AllowsRefStruct()
        {
            builder.Constraints ??= [];
            builder.Constraints.Add(WhereConstraint.AllowsRefStruct);
            return builder;
        }

        public T Constrain(string type)
        {
            builder.TypeConstraints ??= [];
            builder.TypeConstraints.Add(type);
            return builder;
        }

        public T Constrain(ITypeSymbol type) => builder.Constrain(type.ToString());
        
        public string GetConstraints()
        {
            return string.Join(", ", builder.GetPrefixConstraints()
                .Concat(builder.TypeConstraints ?? [])
                .Concat(builder.GetSuffixConstraints()));
        }
    
        private IEnumerable<string> GetPrefixConstraints()
        {
            return builder.Constraints?.Where(c => c is not WhereConstraint.New).Select(GetConstraintsText) ?? [];
        }

        private IEnumerable<string> GetSuffixConstraints()
        {
            return builder.Constraints?.Where(c => c is WhereConstraint.New).Select(GetConstraintsText) ?? [];
        }
    }
    
    public static string GetConstraintsText(this WhereConstraint constraint)
    {
        return constraint switch
        {
            WhereConstraint.Class => "class",
            WhereConstraint.Struct => "struct",
            WhereConstraint.Unmanaged => "unmanaged",
            WhereConstraint.New => "new()",
            WhereConstraint.NotNull => "not null",
            WhereConstraint.AllowsRefStruct => "allow ref struct",
            _ => throw new ArgumentOutOfRangeException(nameof(constraint), constraint, null)
        };
    }
}