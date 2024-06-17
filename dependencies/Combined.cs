using Elements.Geometry;
using Elements.Geometry.Solids;
using OneMesh;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using Namotion.Reflection;

namespace Elements
{
    public partial class Combined : MeshElement
    {
        public Combined(int count)
        {
            GenerateGeometry(count);
        }

        public void GenerateGeometry(int count)
        {
            var random = new Random(21);
            var meshes = new List<Mesh>();
            for (int i = 0; i < count; i++)
            {
                var rectangle = Polygon.Rectangle(random.NextInt64(1, 10), random.NextInt64(1, 10));
                rectangle.Transform(new Transform(new Vector3(random.NextInt64(-20, 20), random.NextInt64(-20, 20), 0)));
                var mass = new Mass(rectangle, random.NextInt64(1, 10), random.NextMaterial());
                meshes.Add(mass.ToMesh());
            }

            this.Mesh = CombineMeshes(meshes, random);
        }

        static Mesh CombineMeshes(List<Mesh> meshes, Random random)
        {
            var combinedMesh = new Mesh();
            var vertexMap = new Dictionary<Geometry.Vertex, Geometry.Vertex>();

            foreach (var mesh in meshes)
            {
                foreach (var vertex in mesh.Vertices)
                {
                    if (!vertexMap.ContainsKey(vertex))
                    {
                        var newVertex = combinedMesh.AddVertex(vertex.Position, vertex.UV, vertex.Normal, random.NextColor(), true);
                        vertexMap[vertex] = newVertex;
                    }
                }

                foreach (var triangle in mesh.Triangles)
                {
                    combinedMesh.AddTriangle(vertexMap[triangle.Vertices[0]],
                                             vertexMap[triangle.Vertices[1]],
                                             vertexMap[triangle.Vertices[2]]);
                }
            }

            return combinedMesh;
        }
    }
}