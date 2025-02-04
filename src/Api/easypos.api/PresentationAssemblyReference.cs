using easypos.application;
using System.Reflection;

namespace easypos.api;
public class PresentationAssemblyReference
{
  internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}