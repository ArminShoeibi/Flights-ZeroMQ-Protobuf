using Flights.Messages;
using Google.Protobuf;
using NetMQ;
using NetMQ.Sockets;
using System;

namespace Flights.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using ResponseSocket responseSocket = new("@tcp://localhost:5500");

            byte[] createFlightDtoBytes = responseSocket.ReceiveFrameBytes();

            CreateFlightDto createFlightDto =
                CreateFlightDto.Parser.ParseFrom(createFlightDtoBytes);

            CreateFlightResultDto createFlightResultDto = new()
            {
                IsSuccessful = true,
                Message = "Flight was successfully added."
            };
            byte[] createFlightResultDtoBytes = createFlightResultDto.ToByteArray();

            responseSocket.SendFrame(createFlightResultDtoBytes, false);
        }
    }
}
