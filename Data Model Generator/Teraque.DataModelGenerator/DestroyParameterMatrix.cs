﻿namespace Teraque.DataModelGenerator
{

	using System;
	using System.CodeDom;
	using System.Collections.Generic;

    /// <summary>
	/// Translates the parameters used for external methods to parameters for internal methods.
	/// </summary>
	/// <copyright>Copyright © 2010-2012 - Teraque, Inc.  All Rights Reserved.</copyright>
	public class DestroyParameterMatrix
	{

		// Public Instance Properties
		public System.Collections.Generic.SortedList<string, ExternalParameterItem> ExternalParameterItems;

		/// <summary>
		/// Creates an object that translates the parameters used for external methods to parameters for internal methods.
		/// </summary>
		public DestroyParameterMatrix(TableSchema tableSchema)
		{

			// Initialize the object.
			this.ExternalParameterItems = new SortedList<string, ExternalParameterItem>();

			// Every set of update parameters requires a unique key to identify the record that is to be updated.
			UniqueConstraintParameterItem primaryKeyParameterItem = new UniqueConstraintParameterItem();
			primaryKeyParameterItem.ActualDataType = typeof(Object[]);
			primaryKeyParameterItem.DeclaredDataType = typeof(Object[]);
			primaryKeyParameterItem.Description = String.Format("The required key for the {0} table.", tableSchema.Name);
			primaryKeyParameterItem.FieldDirection = FieldDirection.In;
			primaryKeyParameterItem.Name = String.Format("{0}Key", CommonConversion.ToCamelCase(tableSchema.Name));
			this.ExternalParameterItems.Add(primaryKeyParameterItem.Name, primaryKeyParameterItem);

			// This will create an interface for the 'Destroy' method.
			foreach (KeyValuePair<string, ColumnSchema> columnPair in tableSchema.Columns)
			{

				// This column is turned into a simple parameter.
				ColumnSchema columnSchema = columnPair.Value;

				// The row version is required for the optimistic concurrency checking that is part of all update operations.
				if (columnSchema.IsRowVersion)
				{
					SimpleParameterItem simpleParameterItem = new SimpleParameterItem();
					simpleParameterItem.ActualDataType = columnSchema.DataType;
					simpleParameterItem.ColumnSchema = columnSchema;
					simpleParameterItem.DeclaredDataType = columnSchema.DataType;
					simpleParameterItem.Description = String.Format("The required value for the {0} column.", CommonConversion.ToCamelCase(columnSchema.Name));
					simpleParameterItem.FieldDirection = FieldDirection.In;
					simpleParameterItem.Name = CommonConversion.ToCamelCase(columnSchema.Name);
					this.ExternalParameterItems.Add(simpleParameterItem.Name, simpleParameterItem);
				}

			}

		}

	}

}