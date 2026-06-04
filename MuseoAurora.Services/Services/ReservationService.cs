using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using MuseoAurora.Models;

namespace MuseoAurora.Services
{
    public class ReservationService : IReservationService
    {
        private readonly string _connectionString;

        public ReservationService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                SELECT r.id, r.participants_count as ParticipantsCount, r.reservation_date as ReservationDate, r.status, r.visitor_id, r.guided_tour_id,
                       v.id, v.first_name as FirstName, v.last_name as LastName, v.email,
                       gt.id, gt.title, gt.description, gt.start_time as StartTime, gt.duration_minutes as DurationMinutes, gt.guide_name as GuideName, gt.max_participants as MaxParticipants, gt.exhibition_id
                FROM reservations r
                INNER JOIN visitors v ON r.visitor_id = v.id
                INNER JOIN guided_tours gt ON r.guided_tour_id = gt.id";

            return await connection.QueryAsync<Reservation, Visitor, GuidedTour, Reservation>(
                query,
                (reservation, visitor, tour) =>
                {
                    reservation.Visitor = visitor;
                    reservation.GuidedTour = tour;
                    return reservation;
                },
                splitOn: "id,id"
            );
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                SELECT r.id, r.participants_count as ParticipantsCount, r.reservation_date as ReservationDate, r.status, r.visitor_id, r.guided_tour_id,
                       v.id, v.first_name as FirstName, v.last_name as LastName, v.email,
                       gt.id, gt.title, gt.description, gt.start_time as StartTime, gt.duration_minutes as DurationMinutes, gt.guide_name as GuideName, gt.max_participants as MaxParticpants, gt.exhibition_id
                FROM reservations r
                INNER JOIN visitors v ON r.visitor_id = v.id
                INNER JOIN guided_tours gt ON r.guided_tour_id = gt.id
                WHERE r.id = @Id"";";
            var reservations = await connection.QueryAsync<Reservation, Visitor, GuidedTour, Reservation>(
                query,
                (reservation, visitor, guidedTour) =>
                {
                    reservation.Visitor = visitor;
                    reservation.GuidedTour = guidedTour;
                    return reservation;
                },
                new { Id = id },
                splitOn: "id,id" 
            );

            return reservations.FirstOrDefault();
        }

        public async Task<InsertResult<Reservation>> CreateReservationAsync(Reservation reservation)
        {
            var result = new InsertResult<Reservation>();
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    INSERT INTO reservations (participants_count, reservation_date, status, visitor_id, guided_tour_id)
                    VALUES (@ParticipantsCount, @ReservationDate, @Status, @VisitorId, @GuidedTourId)
                    RETURNING id;";

                var parameters = new
                {
                    reservation.ParticipantsCount,
                    reservation.ReservationDate,
                    reservation.Status,
                    VisitorId = reservation.Visitor?.Id,
                    GuidedTourId = reservation.GuidedTour?.Id
                };

                reservation.Id = await connection.ExecuteScalarAsync<int>(query, parameters);
                result.Data = reservation;
            }
            catch (NpgsqlException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<bool> UpdateReservationAsync(Reservation reservation)
        {
            var status = true;
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                const string query = @"
                    UPDATE reservations 
                    SET participants_count = @ParticipantsCount, reservation_date = @ReservationDate, 
                        status = @Status, visitor_id = @VisitorId, guided_tour_id = @GuidedTourId
                    WHERE id = @Id";
                var parameters = new
                {
                    reservation.Id,
                    reservation.ParticipantsCount,
                    reservation.ReservationDate,
                    reservation.Status,
                    VisitorId = reservation.Visitor?.Id,
                    GuidedTourId = reservation.GuidedTour?.Id
                };
                status = await connection.ExecuteAsync(query, parameters) > 0;
            }
            catch (NpgsqlException ex)
            {
                return false;
            }
            return status;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE FROM reservations WHERE id = @Id", new { Id = id }) > 0;
        }
    }
}