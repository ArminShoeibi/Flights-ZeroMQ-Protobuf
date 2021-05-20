using Flights.Messages;
using Google.Protobuf;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Threading.Tasks;

namespace Flights.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using RequestSocket requestSocket = new(">tcp://localhost:5500");

            CreateFlightDto createFlightDto = new()
            {
                TravelClass = TravelClass.FirstClass,
                Airline = "SwissAir",
                Destination = "THR",
                Origin = "LX",
                FlightNumber = "SW908",
                ChildFare = 200,
                InfantFare = 180,
                AdultFare = 240,

            };
            byte[] createFlightDtoBytes = createFlightDto.ToByteArray();
            requestSocket.SendFrame(createFlightDtoBytes);

            byte[] createFlightResultBytes = requestSocket.ReceiveFrameBytes();
            CreateFlightResultDto createFlightResultDto = CreateFlightResultDto.Parser.ParseFrom(createFlightResultBytes);

            Console.WriteLine($"Message: {createFlightResultDto.IsSuccessful}");
            Console.WriteLine($"Message: {createFlightResultDto.Message}");


        }
    }
}