#pragma once

#define _USE_MATH_DEFINES

#include <string>
#include "grafos.h"

//#include "restClient.h"

#include "tinyxml2.h"
#include <iostream>
#include <fstream>
#include <sstream> 
#include <math.h>

#define GRAFO_XML_FILE	"DataFiles/Distrito_Porto_Mapa_OSM_Smaller.xml"

#define DEG2RAD(a)		((a) / (180 / M_PI))
#define RAD2DEG(a)		((a) * (180 / M_PI))
#define EARTH_RADIUS	6378.137
#define COORD_SCALE		10

No nos[_MAX_NOS_GRAFO];
Arco arcos[_MAX_ARCOS_GRAFO];
int numNos = 0, numArcos = 0;

using namespace tinyxml2;
using namespace std;

void addNo(No no){
	if(numNos<_MAX_NOS_GRAFO){
		nos[numNos]=no;
		numNos++;
	}else{
		cout << "Número de nós chegou ao limite" << endl;
	}
}

void deleteNo(int indNo){
	if(indNo>=0 && indNo<numNos){
		for(int i=indNo; i<numNos; nos[i++]=nos[i+i]);
		numNos--;
	}else{
		cout << "Indíce de nó inválido" << endl;
	}
}

void imprimeNo(No no){
	cout << "X:" << no.x << "Y:" << no.y << "Z:" << no.z <<endl;
}

void listNos(){
	for(int i=0; i<numNos; imprimeNo(nos[i++]));
}

No criaNo(float x, float y, float z){
	No no;
	no.x=x;
	no.y=y;
	no.z=z;
	return no;
}

void addArco(Arco arco){
	if(numArcos<_MAX_ARCOS_GRAFO){
		arcos[numArcos]=arco;
		numArcos++;
	}else{
		cout << "Número de arcos chegou ao limite" << endl;
	}
}

void deleteArco(int indArco){
	if(indArco>=0 && indArco<numArcos){
		for(int i=indArco; i<numArcos; arcos[i++]=arcos[i+i]);
		numArcos--;
	}else{
		cout << "Indíce de arco inválido" << endl;
	}
}

void imprimeArco(Arco arco){
	cout << "No início:" << arco.noi << "Nó final:" << arco.nof << "Peso:" << arco.peso << "Largura:" << arco.largura << endl;
}

void listArcos(){
	for(int i=0; i<numArcos; imprimeArco(arcos[i++]));
}

Arco criaArco(int noi, int nof, float peso, float largura){
	Arco arco;
	arco.noi=noi;
	arco.nof=nof;
	arco.peso=peso;
	arco.largura=largura;
	return arco;
}

int searchNode(int id)
{
	for (int i = 0; i < numNos; i++)
		if (nos[i].id == id)
			return i;
	return -1;
}

double rescaleValue(double value)
{
	double fractpart, intpart, rescale;
	fractpart = modf(value, &intpart);
	rescale = fractpart / 0.0001;
	return rescale;
}

// Mercator Projection
double lat2y_d(double lat) { return RAD2DEG(log(tan(DEG2RAD(lat) / 2 + M_PI / 4))); }
double lon2x_d(double lon) { return lon; }

double lat2y_m(double lat) { return log(tan(DEG2RAD(lat) / 2 + M_PI / 4)) * EARTH_RADIUS; }
double lon2x_m(double lon) { return DEG2RAD(lon) * EARTH_RADIUS; }

double y2lat_d(double y) { return RAD2DEG(atan(exp(DEG2RAD(y))) * 2 - M_PI / 2); }
double x2lon_d(double x) { return x; }

//void LoadFromAPI(restClient restClient, double coordX1, double coordY1, double coordX2, double coordY2, boolean cart) {
//	
//	string lat1;
//	string long1;
//	string lat2;
//	string long2;

	//if (cart) // Se recebe coordenadas cartesianas
	//{
	//	double y1 = y2lat_d(atof(coordY1.c_str()));
	//	double x1 = x2lon_d(atof(coordX1.c_str()));
	//	double y2 = y2lat_d(atof(coordY2.c_str()));
	//	double x2 = x2lon_d(atof(coordX2.c_str()));
	//	ostringstream convert;
	//	convert << y1;
	//	lat1 = convert.str;
	//	convert << x1;
	//	long1 = convert.str;
	//	convert << y2;
	//	lat2 = convert.str;
	//	convert << x2;
	//	long2 = convert.str;
	//}
	//else {
	//	lat1 = coordY1;
	//	long1 = coordX1;
	//	lat2 = coordY2;
	//	long2 = coordX2;
	//}
	//string content = restClient.getNodes(lat1, long1, lat2, long2);
	//cout << content << endl;
//}

void LoadXmlFile()
{
	XMLDocument xmlDoc;
	XMLError eResult = xmlDoc.LoadFile(GRAFO_XML_FILE);
	if (eResult != XML_SUCCESS)
	{
		cout << "Erro ao abrir ficheiro " << GRAFO_XML_FILE << endl << "Codigo de erro: " << eResult << endl << endl;
		system("pause");
		exit(-1);
	}

	cout << "A carregar dados do ficheiro " << GRAFO_XML_FILE << endl;
	cout << "O carregamento pode demorar alguns minutos, por favor aguarde... ";

	int nodeId = 0, arcoNoIni = 0, arcoNoEnd = 0;
	double nodeLat = 0, nodeLon = 0;

	XMLElement* root = xmlDoc.FirstChildElement("osm");
	for (XMLElement* node = root->FirstChildElement("node"); node != NULL; node = node->NextSiblingElement("node"))
	{
		node->QueryIntAttribute("id", &nodeId);
		node->QueryDoubleAttribute("lat", &nodeLat);
		node->QueryDoubleAttribute("lon", &nodeLon);

		//nos[numNos].id = nodeId;
		//nos[numNos].x = rescaleValue(lon2x_d(nodeLon) * COORD_SCALE);
		//nos[numNos].y = rescaleValue(lat2y_d(nodeLat) * COORD_SCALE);
		//nos[numNos].z = 0;

		//nos[numNos].id = nodeId;
		//nos[numNos].x = rescaleValue(lon2x_m(nodeLon) / COORD_SCALE);
		//nos[numNos].y = rescaleValue(lat2y_m(nodeLat) / COORD_SCALE);
		//nos[numNos].z = 0;

		nos[numNos].id = nodeId;
		nos[numNos].x = rescaleValue(nodeLon) * COORD_SCALE + 64000;
		nos[numNos].y = rescaleValue(nodeLat) * COORD_SCALE - 16000;
		nos[numNos].z = 0;

		nos[numNos].largura = 1.0;
		numNos++;
	}

	for (XMLElement* way = root->FirstChildElement("way"); way != NULL; way = way->NextSiblingElement("way"))
	{
		string name = "Nao disponivel";
		for (XMLElement* tag = way->FirstChildElement("tag"); tag != NULL; tag = tag->NextSiblingElement("tag"))
			if (strcmp(tag->Attribute("k"), "name") == 0)
				name = tag->Attribute("v");

		XMLElement* lastNd = NULL;
		for (XMLElement* nd = way->FirstChildElement("nd"); nd != NULL; nd = nd->NextSiblingElement("nd"))
		{
			if (lastNd == NULL)
				lastNd = nd;
			else
			{
				lastNd->QueryIntAttribute("ref", &arcoNoIni);
				nd->QueryIntAttribute("ref", &arcoNoEnd);

				arcos[numArcos].noi = searchNode(arcoNoIni);
				arcos[numArcos].nof = searchNode(arcoNoEnd);
				arcos[numArcos].peso = 1.0;
				arcos[numArcos].largura = 2.0;
				arcos[numArcos].nome = name;

				lastNd = nd;
				numArcos++;
			}
		}
	}

	// calcula a largura de cada no = maior largura dos arcos que divergem/convergem desse/nesse no	
	//for (int i = 0; i < numNos; i++)
	//{
	//	nos[i].largura = 0;
	//	for (int j = 0; j < numArcos; j++)
	//		if ((arcos[j].noi == i || arcos[j].nof == i) && nos[i].largura < arcos[j].largura)
	//			nos[i].largura = arcos[j].largura;
	//}

	//cout << "INICIO - NOS" << endl;
	//for (int i = 0; i < numNos; i++)
	//	cout << "No ID: " << nos[i].id << endl << "Latitude: " << nos[i].y << endl << "Longitude: " << nos[i].x << endl
	//	<< "Largura: " << nos[i].largura << endl << endl;

	//cout << "INICIO - ARCOS" << endl;
	//for (int i = 0; i < numArcos; i++)
	//	cout << "Arco: " << i << endl << "No inicial: " << arcos[i].noi << endl << "No Final: " << arcos[i].nof << endl 
	//	<< "Nome: " << arcos[i].nome  << endl << "Largura: " << arcos[i].largura << endl << endl;

	cout << "carregado." << endl << endl;

	cout << "DADOS CARREGADOS:" << endl << "TOTAL NOS: " << numNos << "\tTOTAL ARCOS: " << numArcos << endl << endl;
	//system("pause");
}
