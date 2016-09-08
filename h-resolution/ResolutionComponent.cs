namespace Hylasoft.Resolution
{
  /// <summary>
  /// A component of the resolution library.  Capable of identifying itself through overriding object.ToString().
  /// </summary>
  public abstract class ResolutionComponent
  {
    /// <summary>
    /// The name of the component.
    /// </summary>
    protected abstract string ComponentName { get; }

    /// <summary>
    /// Representation of the component's identifying data.
    /// </summary>
    protected abstract string ComponentIdentity { get; }

    public override string ToString()
    {
      const string valueFormat = "{0}: '{1}'";

      if (string.IsNullOrEmpty(ComponentName))
        return base.ToString();

      if (string.IsNullOrEmpty(ComponentIdentity))
        return ComponentName;

      return string.Format(valueFormat, ComponentName, ComponentIdentity);
    }
  }
}
