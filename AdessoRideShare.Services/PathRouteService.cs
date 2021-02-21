using AdessoRideShare.Core;
using ADN.Graphs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Services
{
    public class PathRouteService: IPathRouteService
    {
        private Graph graph;

        public PathRouteService()
        {
            int width = 1000;
            int height = 500;
            int squareSize = 50;

            int xDimension = (width / squareSize);
            int yDimension = (height / squareSize);

            int verticesCount = xDimension * yDimension;

            this.graph = new Graph(verticesCount);

            int right;
            int bottom;
            int left_bottom;
            int right_bottom;
            for(int i=0; i < verticesCount; i++)
            {
                right = i + 1;
                if(right % xDimension != 0 && right < verticesCount) //not right edge
                {
                    this.graph.AddEdge(new Graph.Edge()
                    {
                        Source = i,
                        Destination =right,
                        Weight = 50
                    });
                }

                bottom = i + xDimension;
                if(bottom < verticesCount)
                {
                    this.graph.AddEdge(new Graph.Edge()
                    {
                        Source = i,
                        Destination = bottom,
                        Weight = 50
                    });
                }

                left_bottom = bottom - 1;

                if(bottom % (xDimension) != 0 && left_bottom < verticesCount)
                {
                    this.graph.AddEdge(new Graph.Edge()
                    {
                        Source = i,
                        Destination = left_bottom,
                        Weight = 50
                    });
                }

                right_bottom = bottom + 1;

                if(right_bottom % (xDimension) != 0 && right_bottom < verticesCount)
                {
                    this.graph.AddEdge(new Graph.Edge()
                    {
                        Source = i,
                        Destination = right_bottom,
                        Weight = 50
                    });

                }
            }

        }

        public int[] FindCitiesInRoute(City departureCity, City arrivalCity)
        {
           var sourceNode = departureCity.CityId -1 ; //assumed CityId as index, may be changed.
           var destinationNode = arrivalCity.CityId -1;
           
           var bellmanFord = new BellmanFord(this.graph, sourceNode);
           var result = bellmanFord.GetShortestPath(destinationNode);

            Array.ForEach<int>(result, (value) => // TODO refactor.
            {
                value--;
            });

           return result;
        }
    }
}
