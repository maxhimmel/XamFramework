using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xam.Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true )]
	public class TypeConstraintAttribute : PropertyAttribute
	{
		public Type Constraint { get; }

		public TypeConstraintAttribute( Type constraint )
		{
			Constraint = constraint;
		}
	}
}