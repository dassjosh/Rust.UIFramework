using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class ContentSizeFitterComponent : IContentSizeFitterComponent, IContentSizeFitterComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ContentSizeFitter.FitMode> _horizontalFit = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ContentSizeFitterData.HorizontalFit);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ContentSizeFitter.FitMode> _verticalFit = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ContentSizeFitterData.VerticalFit);

	public partial UnityEngine.UI.ContentSizeFitter.FitMode HorizontalFit { get => _horizontalFit.Value; set => _horizontalFit.Value = value; }
	public partial UnityEngine.UI.ContentSizeFitter.FitMode VerticalFit { get => _verticalFit.Value; set => _verticalFit.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ContentSizeFitter.FitMode> IContentSizeFitterComponentTrackable.HorizontalFit => _horizontalFit;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ContentSizeFitter.FitMode> IContentSizeFitterComponentTrackable.VerticalFit => _verticalFit;

	public Oxide.Ext.UiFramework.Components.ContentSizeFitterComponent SetHorizontalFit(UnityEngine.UI.ContentSizeFitter.FitMode horizontalFit)
	{
		HorizontalFit = horizontalFit;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ContentSizeFitterComponent SetVerticalFit(UnityEngine.UI.ContentSizeFitter.FitMode verticalFit)
	{
		VerticalFit = verticalFit;
		return this;
	}
	public IContentSizeFitterComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_horizontalFit.HasChanged || _verticalFit.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_horizontalFit.ResetHasChanged();
		_verticalFit.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_horizontalFit.Reset();
		_verticalFit.Reset();
	}
}


