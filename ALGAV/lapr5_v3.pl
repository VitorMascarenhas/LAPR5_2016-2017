use_module(library(date)).

%dependencies
:-[baseConhecimentoTmp].


%horaInicial(Hora inicial da visita).
horaInicial('9:00').

%pontoPartida(Inicio).
pontoPartida(1).

%circuitoFechado(True se o visitante pretende terminar o percurso no ponto de origem).
circuitoFechado('True').

%tempoVisita(4 ou 8 horas)
tempoVisita(8).

%velocidade_media(Km/ h).
velocidade_media(5).


%Gerar uma lista dos POIs a visitar
gerarListaPOIs(ListaPoisOrdenada):-
	findall((AberturaStamp,Local,TempoVisitaStamp,FechoStamp,Id),
		(poi(Id,Local,Abertura,Fecho,TempoVisita),
		hora_stamp(Abertura,AberturaStamp),
		hora_stamp(Fecho,FechoStamp),
		hora_stamp(TempoVisita,TempoVisitaStamp)),
		ListaPOIS),
	sort(ListaPOIS,ListaPoisOrdenada),
	escreverListaPOIs(ListaPoisOrdenada),nl.



estimativa(Origem,Destino,Estimativa):-
	no(Origem,XO,YO,_),
	no(Destino,XD,YD,_),
	DX is XO - XD,
	DY is YO - YD,
	Estimativa is sqrt((DX^2) + (DY^2)).


removeUltimoElemento([X|Y],Z):-
	removeElemento(Y,Z,X).

removeElemento([],[],_).
removeElemento([X|Y],[X0|Z],X0):-
	removeElemento(Y,Z,X).

ultimoElemento([Last], Last).
ultimoElemento([_ | Rest], Last):-
	ultimoElemento(Rest, Last), !.
	
removePrimeiroElemento([_|Resto],Resto).

calculaTempoDeslocacao(Peso,HoraDia,TempoDeslocacao,HoraComDeslocacao):-
	velocidade_media(Velocidade),
	DeslocacaoMin is ((Peso / 25) / Velocidade ) * 60,
	adicionarMinutos(HoraDia,round(DeslocacaoMin),HoraComDeslocacao),
	hora_stamp("00:00", HoraZero),
	adicionarMinutos(HoraZero,round(DeslocacaoMin),TempoDeslocacao).


retornarPontoOrigem('True',PontoPartida,Destino,Caminho,HoraAtualizada):-
	removePrimeiroElemento(Caminho,CaminhoSemPrimeiroElemento),
	reverse(CaminhoSemPrimeiroElemento,CaminhoInvertido),
	estimativa(PontoPartida,Destino,Estimativa),
	gerarPercurso2([(Estimativa,0,[PontoPartida])],Destino,NovoCaminho,Peso),
	reverse(NovoCaminho, NovoCaminhoInvertido),
	calculaTempoDeslocacao(Peso,HoraAtualizada,TempoDeslocacao,_),
	converteHorasEmMinutos(TempoDeslocacao,DeslocacaoMin),
	adicionarMinutos(HoraAtualizada,round(DeslocacaoMin),HoraTotal),
	escreverPercursoRetorno(NovoCaminhoInvertido,HoraAtualizada,TempoDeslocacao,HoraTotal),
	append(CaminhoInvertido,NovoCaminhoInvertido,CaminhoComRetorno),
	escreverPercursoCompletoComRetorno(CaminhoComRetorno,HoraTotal).

retornarPontoOrigem('False',_,_,Caminho,HoraCompleta):-
	reverse(Caminho,CaminhoInvertido),
	escreverPercursoCompletoSemRetorno(CaminhoInvertido,HoraCompleta).


percursoEntrePois(_,[],_,HoraAtualizada,HoraCompleta):-
		HoraCompleta is HoraAtualizada.

percursoEntrePois(PontoPartida,[(Abertura,Destino,TempoVisita,Fecho,_)|Resto],TodosCaminhos,HoraDia,HoraDiaCompleta):-
	estimativa(PontoPartida,Destino,Estimativa),
	gerarPercurso2([(Estimativa,0,[PontoPartida])],Destino,NovoCaminho,Peso),
	calculaTempoDeslocacao(Peso,HoraDia,TempoDeslocacao,HoraComDeslocacao),
	HoraComDeslocacao >= Abertura,
	HoraComDeslocacao =< Fecho,
	converteHorasEmMinutos(TempoVisita,TempoVisitaMin),
	adicionarMinutos(HoraComDeslocacao,TempoVisitaMin,HoraDiaAtualizada),
	escreverVisita(NovoCaminho,HoraDia,TempoVisita,TempoDeslocacao,HoraDiaAtualizada),
	removeUltimoElemento(NovoCaminho,NovoCaminhoSemRepetido),
	percursoEntrePois(Destino,Resto,Caminho,HoraDiaAtualizada,HoraDiaCompleta),
	append(Caminho,NovoCaminhoSemRepetido,TodosCaminhos).


gerarPercurso():-
	pontoPartida(PontoPartida),
	horaInicial(HoraInicial),
	hora_stamp(HoraInicial,HoraDia),
	circuitoFechado(Estado),
	escreverInicio(PontoPartida,HoraDia),
	gerarListaPOIs(ListaPOIs),
	percursoEntrePois(PontoPartida,ListaPOIs,Caminho,HoraDia,HoraCompleta),
	append(Caminho,[PontoPartida],CaminhoCorrigido),
	nth0(0, CaminhoCorrigido, UltimoDestino),
	retornarPontoOrigem(Estado,UltimoDestino,PontoPartida,CaminhoCorrigido,HoraCompleta).


gerarPercurso2([(_,PesoTotal,[Destino|R])|_],Destino,[Destino|R],PesoTotal).

gerarPercurso2([(_,Peso,[X|R1])|OutrosCaminhos],Destino,Caminho,PesoTotal):-
	findall((Estimativa,Np,[Z,X|R1]),
		(arco(X,Z,A),
		not(member(Z,R1)),
		Np is Peso + A,
		estimativa(Z,Destino,Estimativa)),
		NovosCaminhos),
	append(OutrosCaminhos,NovosCaminhos,TodosCaminhos),
	sort(TodosCaminhos,TodosCaminhosOrdenados),
	gerarPercurso2(TodosCaminhosOrdenados,Destino,Caminho,PesoTotal).



hora_stamp(HorasStr,TimeStamp):-
	split_string(HorasStr, ":", "", HoraLista),
	nth0(0, HoraLista, HoraStr),
	atom_number(HoraStr, Hora),
	nth0(1, HoraLista, MinStr),
	atom_number(MinStr, Min),
	date_time_stamp(date(0001,01,01,Hora,Min,0,0,'UTC',-), TimeStamp).

adicionarMinutos(TimeStamp,Minutos,NewTimeStamp):-
	stamp_date_time(TimeStamp, date(A,M,D,H,Min,_,_,_,_),'UTC'),
	Min_tmp is Min + Minutos,
	date_time_stamp(date(A,M,D,H,Min_tmp,0,0,'UTC',-), NewTimeStamp).

converteHorasEmMinutos(HoraStr, Minutos):-
	stamp_date_time(HoraStr, date(_,_,_,H,Min,_,_,_,_),'UTC'),
	Minutos is H * 60 + Min.	



escreverHora(TimeStamp):-
	stamp_date_time(TimeStamp, date(_,_,_,H,M,_,_,_,_), 'UTC'),
	atomics_to_string([H,":",M,"h"],NovaHora),
	write(NovaHora).


escreverInicio(PontoPartida,HoraInicialStamp):-
	nl,write('Ponto de partida: '), write(PontoPartida),nl,
	write('Hora de inicio: '), escreverHora(HoraInicialStamp), nl, nl,
	write('-------------- Pontos de Interesse a visitar ---------------'), nl.

	
escreverListaPOIs([(Abertura,Local,TempoVisita,Fecho,_)|[]]):-
	!,write('Local: '),write(Local),nl,
	write('Abertura: '),escreverHora(Abertura),nl,
	write('Fecho: '),escreverHora(Fecho),nl,
	write('Tempo visita: '),escreverHora(TempoVisita), nl, nl,
	write('--------------- Inicio do percurso sugerido ----------------'), nl.

escreverListaPOIs([(Abertura,Local,TempoVisita,Fecho,_)|T]):-
	write('Local: '),write(Local),nl,
	write('Abertura: '),escreverHora(Abertura),nl,
	write('Fecho: '),escreverHora(Fecho),nl,
	write('Tempo visita: '),escreverHora(TempoVisita),nl,nl,
	escreverListaPOIs(T).


escreverVisita(Caminho,HoraDia,TempoVisita,HoraDiaComDeslocacao,HoraCompleta):-
	reverse(Caminho,CaminhoInvertido),
	write('Percurso: '), escreverPercurso(CaminhoInvertido),
	write('Hora atual: '), escreverHora(HoraDia),nl,
	write('Tempo de deslocacao: '), escreverHora(HoraDiaComDeslocacao),nl,
	write('Tempo de visita: '), escreverHora(TempoVisita),nl,
	write('Hora de fim de visita: '), escreverHora(HoraCompleta),nl,nl.

	
escreverPercursoRetorno(Caminho,HoraAtual,TempoDeslocacao,HoraTotal):-
	write('Percurso: '), escreverPercurso(Caminho),
	write('Hora atual: '), escreverHora(HoraAtual),nl,
	write('Tempo de deslocacao: '), escreverHora(TempoDeslocacao),nl,
	write('Hora do dia: '), escreverHora(HoraTotal),nl,nl.


escreverPercursoCompletoComRetorno(Caminho,HoraInicialComDeslocacao):-
	write('------ Percurso completo com retorno ao ponto partida ------'),
	nl,nl,escreverPercurso(Caminho,HoraInicialComDeslocacao),nl,nl.

escreverPercursoCompletoSemRetorno(Caminho,HoraInicialComDeslocacao):-
	write('------ Percurso completo sem retorno ao ponto partida ------'),
	nl,nl,escreverPercurso(Caminho,HoraInicialComDeslocacao),nl,nl.


escreverPercurso([Caminho|[]],DuracaoVisita):-
	!,write(Caminho),nl,nl,
	write('Fim da visita: '),
	escreverHora(DuracaoVisita).

escreverPercurso([Caminho|T],DuracaoVisita):-
	write(Caminho),write(" -> "),
	escreverPercurso(T,DuracaoVisita).
	

escreverPercurso([Caminho|[]]):-
	!,write(Caminho), nl.

escreverPercurso([Caminho|T]):-
	write(Caminho),write(" -> "),
	escreverPercurso(T).
