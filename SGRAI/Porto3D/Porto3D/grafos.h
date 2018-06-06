#ifndef _GRAFO_INCLUDE
#define _GRAFO_INCLUDE

#define _MAX_NOS_GRAFO 250000
#define _MAX_ARCOS_GRAFO 350000

#define NORTE_SUL	0
#define ESTE_OESTE	1
#define PLANO		2

//#include<restClient.h>

typedef struct No
{
	int id;
	double x, y, z;
	float largura;
}No;

typedef struct Arco
{
	int noi,nof;
	float peso,largura;
	std::string nome;
}Arco;

extern No nos[];
extern Arco arcos[];
extern int numNos, numArcos;

void addNo(No);
void deleteNo(int);
void imprimeNo(No);
void listNos();
No criaNo(float, float, float);

void addArco(Arco);
void deleteArco(int);
void imprimeArco(Arco);
void listArcos();
Arco criaArco(int, int, float, float);

int searchNode(int id);
double rescaleValue(double value);
//void LoadFromAPI(restClient restClient, double coordX1, double coordY1, double coordX2, double coordY2, boolean cart);
void LoadXmlFile();

#endif