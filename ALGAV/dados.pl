:-[lapr5v2].
:- use_module(library(writef)).

open_db:-odbc_connect('lapr5',_,[
	user(prolog),
	password(prolog),
	alias(lapr5_sqlexpress),
	open(once),
	mars(true)]).

percurso(Visit):-retract_data,visit_data(Visit),roads_data,pois_data(Visit),!,
	gerarPercurso.

% ----- LIMPAR BASE DE CONHECIMENTO -----
retract_data:-
	write('Retracting data'),nl,
	retractall(poi(_,_,_,_,_)), retractall(arco(_,_,_)),retractall(estrada(_,_)),
	retractall(pontoPartida(_)), retractall(horaInicial(_)),
	retractall(tempoVisita(_)), retractall(circuitoFechado(_)).

% -----INSERIR DADOS -----
% VISITA
visit_data(Id):-
	write('Populating visit data'),nl,
	visit(Id,StartDate,ReturnToStart,StartLocation_Id,Duration),
	location(StartLocation_Id,Coordinates),
	get_start_connection(Coordinates),
	write_return(ReturnToStart), assertz(pontoPartida(Coordinates)),
	split_string(StartDate, " ", "", DateTime),
	nth0(1, DateTime, HoraStr),
	assertz(horaInicial(HoraStr)), assertz(tempoVisita(Duration)).

write_return(1):-assertz(circuitoFechado('True')).
write_return(_):-assertz(circuitoFechado('False')).

get_start_connection(Coordinates):-
	roadGpsCoordinate(_,Coordinates);
	(gpsCoordinate(Coordinates,Lat,Long,_), get_closest_no(Coordinates,Lat,Long,Closest),roadGpsCoordinate(_,Closest),
	assertz(arco(Coordinates,Closest,4))).

% ESTRADAS - COORDENADAS E LIGACOES
roads_data:-
	write('Populating roads data'),nl,
	allRoad(List),write_roads(List).

write_roads([]).
write_roads([(Id,Weight)|T]):-
	road_connections(Id,Weight),write_roads(T).

road_connections(Id,Weight):-
	gpsCoordinate_road(List,Id),conRoad(List,Weight).

%conRoad([],_).
conRoad([_|[]],_).
conRoad([Coord1|[Coord2|T]],Weight):-
	(arco(Coord1,Coord2,Weight);assertz(arco(Coord1,Coord2,Weight))),conRoad([Coord2|T],Weight).

% POIS
pois_data(Visit):-
	write('Populating pois data'),nl,
	pointOfInterest_visit(Ids,Visit),get_pois(Ids).

get_pois([]).
get_pois([Id|T]):-
	pointOfInterest(Id,BusinessHours_FromHour,BusinessHours_ToHour,LocationId,TimeToVisit), TimeHour is float_integer_part(TimeToVisit),
	TimeMinute is float_fractional_part(TimeToVisit),	
	string_concat(TimeHour, ":",F1), string_concat(F1, TimeMinute,F2),
	location(LocationId,CoordinatesId),
	gpsCoordinate(CoordinatesId,Latitude,Longitude,_),
	assertz(poi(Id,CoordinatesId,BusinessHours_FromHour,BusinessHours_ToHour, F2)),
	get_closest_no(Closest,Latitude,Longitude,CoordinatesId),
	assertz(arco(Closest,CoordinatesId,4)),get_pois(T).


get_closest_no(Id,Lat,Long,Location):-
	atom_string(Lat,Str1), atom_string(Long,Str2),atom_string(Location,Str3),
	string_concat('SELECT [Id],SQRT(POWER(',Str1,F1),
	string_concat(F1,'-Latitude,2)+POWER(',F2),string_concat(F2,Str2,F3),
	string_concat(F3,'-Longitude,2)) AS [Distancia] FROM GpsCoordinate WHERE [Id] <> ',F4),
	string_concat(F4,Str3,F5),
	string_concat(F5,' ORDER BY Distancia ASC',F6),
	odbc_query(lapr5_sqlexpress,F6,row(Id,_),[types([integer,float])]),roadGpsCoordinate(_,Id).

% --- odbc ---

%GpsCoordinate
gpsCoordinate(Id,Latitude,Longitude,Altitude):-
	odbc_query(lapr5_sqlexpress, 'SELECT [Id],[Latitude],[Longitude],[Altitude] FROM GpsCoordinate', row(Id, Latitude, Longitude, Altitude), [types([integer,float,float,float])]).

%Location
location(Id,Coordinates_Id):-
	odbc_query(lapr5_sqlexpress, 'SELECT [Id],[Coordinates_Id] FROM Location', row(Id, Coordinates_Id), [types([integer,integer])]).

%PointOfInterest
pointOfInterest(Id,BusinessHours_FromHour,BusinessHours_ToHour,LocationId,TimeToVisit):-
	% BusinessHours - ex. "12:00:00.0000000"
	odbc_query(lapr5_sqlexpress, 'SELECT [Id],[BusinessHours_FromHour],[BusinessHours_ToHour],[LocationId],[TimeToVisit] FROM PointOfInterest', row(Id,BusinessHours_FromHour,BusinessHours_ToHour,LocationId,TimeToVisit), [types([integer,string,string,integer,float])]).

%Road
road(Id,Weight):-
	odbc_query(lapr5_sqlexpress, 'SELECT [Id],[Weight] FROM Road', row(Id,Weight), [types([integer,integer])]).

%RoadGpsCoordinate
roadGpsCoordinate(Road_Id,GpsCoordinate_Id):-
	odbc_query(lapr5_sqlexpress, 'SELECT [Road_Id],[GpsCoordinate_Id] FROM RoadGpsCoordinate', row(Road_Id, GpsCoordinate_Id), [types([integer,integer])]).

%Route
route(Id,VisitId):-
	odbc_query(lapr5_sqlexpress, 'SELECT [Id],[VisitId] FROM Route', row(Id,VisitId), [types([integer,integer])]).

%RouteRoad
routeRoad(Route_Id, Road_Id):-
	odbc_query(lapr5_sqlexpress, 'SELECT [Route_Id],[Road_Id] FROM RouteRoad', row(Route_Id,Road_Id), [types([integer,integer])]).

%Visit
visit(Id,StartDate,ReturnToStart,StartLocation_Id,Duration):-
	odbc_query(lapr5_sqlexpress, 'SELECT [Id],[StartDate],[ReturnToStart],[StartLocation_Id],[Duration] FROM Visit', row(Id, StartDate, ReturnToStart, StartLocation_Id, Duration), [types([integer,string,default,integer,integer])]).

%VisitPointOfInterest
visitPointOfInterest(PointOfInterest_Id, Visit_Id):-
	odbc_query(lapr5_sqlexpress, 'SELECT [Visit_Id],[PointOfInterest_Id] FROM VisitPointOfInterest', row(Visit_Id, PointOfInterest_Id), [types([integer,integer])]).


%All GpsCoordinate
allGpsCoordinate(List):-
	findall((Id,Latitude,Longitude,Altitude),gpsCoordinate(Id,Latitude,Longitude,Altitude),List).

%All Location
allLocation(List):-
	findall((Id,Coordinates_Id),location(Id,Coordinates_Id),List).

%All PointOfInterest
allPointOfInterest(List):-
	findall((Id,BusinessHours_FromHour,BusinessHours_ToHour,LocationId,TimeToVisit),
	pointOfInterest(Id,BusinessHours_FromHour,BusinessHours_ToHour,LocationId,TimeToVisit),List).

%All Road
allRoad(List):-
	findall((Id,Weight), road(Id,Weight),List).

%All RoadGpsCoordinate
allRoadGpsCoordinate(List):-
	findall((Road_Id,GpsCoordinate_Id),roadGpsCoordinate(Road_Id,GpsCoordinate_Id),List).

gpsCoordinate_road(List,Road_Id):-
	findall(GpsCoordinate_Id,roadGpsCoordinate(Road_Id,GpsCoordinate_Id),List).

%All Route
allRoute(List):-
	findall((Id,VisitId),route(Id,VisitId),List).

route_visit(List,VisitId):-
	findall(Id,route(Id,VisitId),List).

%All RouteRoad
allRouteRoad(List):-
	findall((Route_Id, Road_Id),routeRoad(Route_Id, Road_Id),List).

road_route(List,Route_Id):-
	findall(Road_Id,routeRoad(Route_Id, Road_Id),List).

%All Visit
allVisit(List):-
	findall((Id,StartDate,ReturnToStart,StartLocation_Id,Duration),visit(Id,StartDate,ReturnToStart,StartLocation_Id,Duration),List).

%All VisitPointOfInterest
allVisitPointOfInterest(List):-
	findall((PointOfInterest_Id, Visit_Id), visitPointOfInterest(PointOfInterest_Id, Visit_Id),List).

pointOfInterest_visit(List,Visit_Id):-
	findall(PointOfInterest_Id, visitPointOfInterest(PointOfInterest_Id, Visit_Id),List).