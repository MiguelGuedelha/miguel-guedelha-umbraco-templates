//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Models
{
	/// <summary>Heading with Links</summary>
	[PublishedModel("headingWithLinks")]
	public partial class HeadingWithLinks : PublishedElementModel, IMandatoryHeading
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "")]
		public new const string ModelTypeAlias = "headingWithLinks";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedContentTypeCache contentTypeCache)
			=> PublishedModelUtility.GetModelContentType(contentTypeCache, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedContentTypeCache contentTypeCache, Expression<Func<HeadingWithLinks, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(contentTypeCache), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public HeadingWithLinks(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Links
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("links")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.Link> Links => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.Link>>(_publishedValueFallback, "links");

		///<summary>
		/// Heading
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("heading")]
		public virtual string Heading => global::UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Models.MandatoryHeading.GetHeading(this, _publishedValueFallback);

		///<summary>
		/// Heading Size
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("headingSize")]
		public virtual string HeadingSize => global::UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Models.MandatoryHeading.GetHeadingSize(this, _publishedValueFallback);
	}
}
