%###Inicio_Server###
:-[dados].
:- use_module(library(http/thread_httpd)).
:- use_module(library(http/http_dispatch)).
:- use_module(library(http/http_parameters)).
:-use_module(library(http/http_error)).
:-use_module(library(http/http_header)).
:-use_module(library(odbc)).


% --- Inicio pedidos ---
% how to test:
% http_client:http_post('http://localhost:8082/percurso',form_data([visita=1]),Reply,[]).

:- http_handler('/percurso', percurso_visit, []), open_db.

% --- gestao de pedidos ---
percurso_visit(Request) :-
	http_parameters(Request, [visita(Visita, [integer])]), percurso(Visita), format('Content-type: text/plain~n~n'),
	format('Received').
