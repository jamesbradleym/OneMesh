using Elements;
using Elements.Geometry;
using System.Collections.Generic;

namespace OneMesh
{
  public static class OneMesh
  {
    /// <summary>
    /// The OneMesh function.
    /// </summary>
    /// <param name="model">The input model.</param>
    /// <param name="input">The arguments to the execution.</param>
    /// <returns>A OneMeshOutputs instance containing computed results and the model with any new elements.</returns>
    public static OneMeshOutputs Execute(Dictionary<string, Model> inputModels, OneMeshInputs input)
    {
      var output = new OneMeshOutputs();
      var meshy = new Combined(Convert.ToInt32(input.Count));
      output.Model.AddElement(meshy);
      return output;
    }
  }
}