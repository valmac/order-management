﻿namespace Teraque.DataModelGenerator
{

	using System;
	using System.CodeDom;
	using System.ServiceModel;

	/// <summary>
	/// Creates a 'Record is busy' Exception for the primary key of a table.
	/// </summary>
	class CodeThrowConcurrencyExceptionStatement : CodeThrowExceptionStatement
	{

		/// <summary>
		/// Creates a 'Record is busy' Exception for the primary key of a table.
		/// </summary>
		/// <param name="tableSchema">The table where the error occured.</param>
		public CodeThrowConcurrencyExceptionStatement(TableSchema tableSchema, CodeExpression keyExpression)
		{

			//                throw new global::System.ServiceModel.FaultException<OptimisticConcurrencyFault>(new global::Teraque.OptimisticConcurrencyFault("Attempt to access a Employee record ({0}) that doesn\'t exist", employeeIdKeyText));
			this.ToThrow = new CodeObjectCreateExpression(new CodeGlobalTypeReference(typeof(FaultException<OptimisticConcurrencyFault>)),
				new CodeObjectCreateExpression(new CodeGlobalTypeReference(typeof(OptimisticConcurrencyFault)), new CodePrimitiveExpression(tableSchema.Name), keyExpression));

		}

	}
}