-- Pulisce le tabelle esistenti e resetta i contatori degli ID (Serial/Identity)
TRUNCATE TABLE tickets, reservations, ticket_types, visitors, guided_tours, artworks, exhibitions 
RESTART IDENTITY CASCADE;

-------------------------------------------------------------------------------
-- 1. EXHIBITIONS (Mostre)
-------------------------------------------------------------------------------
INSERT INTO exhibitions (title, description, start_date, end_date, status) VALUES
('Rinascimento Segreto', 'Capolavori mai visti di maestri fiorentini del XV secolo.', '2026-01-15', '2026-06-30', 'Active'),
('L''Ombra del Faraone', 'Reperti straordinari dall''antico Egitto e della dinastia di Tutankhamon.', '2026-04-01', '2026-10-31', 'Active'),
('Futurismo e Avanguardia', 'L''evoluzione dell''arte italiana nei primi del Novecento.', '2026-09-01', '2027-01-15', 'Planned'),
('Leonardo: Il Genio', 'Mostra interattiva sulle macchine da guerra e i codici di Leonardo.', '2025-05-01', '2025-11-30', 'Closed');

-------------------------------------------------------------------------------
-- 2. ARTWORKS (Opere d'Arte)
-------------------------------------------------------------------------------
-- Assumiamo: Id 1 = Rinascimento, Id 2 = Egitto, Id 3 = Futurismo
INSERT INTO artworks (exhibition_id, title, author, year) VALUES
(1, 'Madonna col Bambino', 'Scuola di Filippo Lippi', 1465),
(1, 'Ritratto di Gentiluomo', 'Attribuito a Botticelli', 1482),
(2, 'Il Sarcofago di Khnumhotep', 'Ignoto Antico Egiziano', -1850),
(2, 'Amuleto dell''Occhio di Horus', 'Artigiano Reale', -1323),
(3, 'Forme Uniche della Continuità nello Spazio', 'Umberto Boccioni', 1913),
(3, 'Velocità d''automobile', 'Giacomo Balla', 1913);

-------------------------------------------------------------------------------
-- 3. GUIDED TOURS (Visite Guidate)
-------------------------------------------------------------------------------
INSERT INTO guided_tours (exhibition_id, title, start_time, duration_minutes, guide_name, max_participants) VALUES
(1, 'Tour Serale: I Segreti del Rinascimento', '2026-06-10 21:00:00', 90, 'Marco Basaiti', 25),
(1, 'Visita Guidata per Scuole - XV Secolo', '2026-06-12 10:00:00', 60, 'Elena Cornaro', 30),
(2, 'Misteri e Maledizioni dei Faraoni', '2026-06-15 15:30:00', 75, 'Archeologo Alberto Angela', 20),
(3, 'Anteprima Esclusiva: Il Movimento Futurista', '2026-08-28 18:00:00', 90, 'Filippo Tommaso', 15);

-------------------------------------------------------------------------------
-- 4. VISITORS (Visitatori)
-------------------------------------------------------------------------------
INSERT INTO visitors (first_name, last_name, email) VALUES
('Mario', 'Rossi', 'mario.rossi@example.com'),
('Giulia', 'Bianchi', 'giulia.bianchi@example.com'),
('Luca', 'Verdi', 'luca.verdi@example.com'),
('Anna', 'Neri', 'anna.neri@example.com'),
('John', 'Doe', 'john.doe@example.com');

-------------------------------------------------------------------------------
-- 5. TICKET TYPES (Tipologie Biglietto)
-------------------------------------------------------------------------------
INSERT INTO ticket_types (name, price) VALUES
('Intero Intero', 15.00),
('Ridotto (Under 26 / Over 65)', 10.00),
('Gruppi (Min 10 persone)', 12.00),
('Studente Universitario', 8.00),
('Gratuito (Disabili / Giornalisti)', 0.00);

-------------------------------------------------------------------------------
-- 6. RESERVATIONS (Prenotazioni alle Visite Guidate)
-------------------------------------------------------------------------------
-- Assumiamo: visitatori da 1 a 5, tour da 1 a 4
INSERT INTO reservations (visitor_id, guided_tour_id, participants_count, reservation_date, status) VALUES
(1, 1, 2, '2026-06-01 10:30:00', 'Confirmed'),
(2, 1, 1, '2026-06-02 14:15:00', 'Confirmed'),
(3, 3, 4, '2026-06-03 09:00:00', 'Pending'),
(4, 4, 2, '2026-05-25 18:45:00', 'Confirmed');

-------------------------------------------------------------------------------
-------------------------------------------------------------------------------
-- 7. TICKETS (Biglietti Emessi) - CORRETTO (Senza NULL)
-------------------------------------------------------------------------------
INSERT INTO tickets (visitor_id, ticket_type_id, exhibition_id, guided_tour_id, quantity, total_price, purchase_date) VALUES
(1, 1, 1, 1, 2, 30.00, '2026-06-01 10:32:00'), -- Mario Rossi: 2 Interi (Rinascimento + Tour 1)
(2, 2, 1, 1, 1, 10.00, '2026-06-02 14:16:00'), -- Giulia Bianchi: 1 Ridotto (Rinascimento + Tour 1)
(3, 3, 2, 3, 4, 48.00, '2026-06-03 09:05:00'), -- Luca Verdi: 4 biglietti Gruppo (Egitto + Tour 3)
(5, 4, 3, 4, 1, 8.00,  '2026-06-03 11:00:00'); -- John Doe: 1 Studente (Futurismo + Tour 4)