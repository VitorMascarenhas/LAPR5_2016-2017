:- use_module(library(http/thread_httpd)).
:- use_module(library(http/http_dispatch)).
:- use_module(library(http/http_parameters)).
:-use_module(library(http/http_error)).
:-use_module(library(http/http_header)).
:-[server].

main:-
	pce_main_loop(main).

main(_):-http_server(http_dispatch, [port(8082)]),open_db.

save(Exe) :-
	pce_autoload_all,
	qsave_program(Exe,
		[ global(1048576),
		goal(main) ]).
