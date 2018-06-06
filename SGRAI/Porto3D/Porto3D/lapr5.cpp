#pragma once
#pragma warning(disable : 4838)

#define _USE_MATH_DEFINES

#include <iostream>
#include <string>
#include <math.h>
#include <stdlib.h>
#include <vector>

#include <GL/glaux.h>
#include <GL/glut.h>
#include <gl/gl.h>
#include <gl/glu.h>
#include "grafos.h"

#include "mathlib.h"
#include "studio.h"
#include "mdlviewer.h"
#include "Model_3DS.h"
#include <ALUT/AL/alut.h>

#include "restClient.h"

#include "alc.h"

#pragma comment (lib, "glaux.lib")    /* link with Win32 GLAUX lib usada para ler bitmaps */
#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glu32.lib")
#pragma comment(lib, "libcurl_imp.lib") /* Livraria do Curl*/

using namespace std;


#define graus(X)			(double)((X)*180/M_PI)
#define rad(X)				(double)((X)*M_PI/180)

//Variaveis da cena
#define FLOOR_GAP			500
#define WINDOW_GAP			20
#define FLOOR_STEP			100
#define MODEL_SCALE			5
#define MAX_POIS			10
#define ROUTE_HEIGHT		1.2

//Texturas da cena
#define NUM_TEXTURES		8
#define ID_FLOOR_TEX		0
#define FLOOR_TEXTURE		"Textures/Grass.bmp"
#define ID_ROAD_TEX			1
#define ROAD_TEXTURE		"Textures/Road.bmp"
#define ID_NODE_TEX			2
#define NODE_TEXTURE		"Textures/Cross-Road.bmp"

//Skybox
#define GL_CLAMP_TO_EDGE	0x812F
#define ID_SB_FRONT			3
#define FRONT_TEXTURE		"Textures/SB-Front.bmp"
#define ID_SB_RIGHT			4
#define RIGHT_TEXTURE		"Textures/SB-Right.bmp"
#define ID_SB_BACK			5
#define BACK_TEXTURE		"Textures/SB-Back.bmp"
#define ID_SB_LEFT			6
#define LEFT_TEXTURE		"Textures/SB-Left.bmp"
#define ID_SB_TOP			7
#define TOP_TEXTURE			"Textures/SB-Top.bmp"

//Variaveis dos modelos .mdl
#define VISITOR_MODEL		"Models/Visitor/homer.mdl"
#define VISITOR_SCALE		0.05
#define OBJECTO_ALTURA		1.8
#define OBJECTO_VELOCIDADE	10
#define OBJECTO_ROTACAO		20
#define OBJECTO_RAIO		0.12
#define EYE_ROTACAO			1
#define FOUNTAIN_MODEL		"Models/Fountain/Brazier01.mdl"
#define FOUNTAIN_SCALE		0.005
#define LAMP_MODEL			"Models/StreetLamp/lampara2.mdl"

//Variaveis de acesso a WebAPI
#define WEBAPI				"http://10.8.11.147"
#define RAIO_ACCAO			10

#define MAX_CHARS		256
#define FONT_EXTRUDE	0.4

GLYPHMETRICSFLOAT g_GlyphInfo[MAX_CHARS];
HDC	g_hDC;
UINT g_FontListID = 0;
HFONT hOldFont;


// luzes e materiais
const GLfloat mat_ambient[][4] = { {0.33, 0.22, 0.03, 1.0},		// brass
								  {0.0, 0.0, 0.0},				// red plastic
								  {0.0215, 0.1745, 0.0215},		// emerald
								  {0.02, 0.02, 0.02},			// slate
								  {0.0, 0.0, 0.1745},			// azul
								  {0.02, 0.02, 0.02},			// preto
								  {0.1745, 0.1745, 0.1745} };	// cinza

const GLfloat mat_diffuse[][4] = { {0.78, 0.57, 0.11, 1.0},			// brass
								  {0.5, 0.0, 0.0},					// red plastic
								  {0.07568, 0.61424, 0.07568},		// emerald
								  {0.78, 0.78, 0.78},				// slate
								  {0.0, 0.0,  0.61424},				// azul
								  {0.08, 0.08, 0.08},				// preto
								  {0.61424, 0.61424, 0.61424} };	// cinza

const GLfloat mat_specular[][4] = { {0.99, 0.91, 0.81, 1.0},			// brass
								   {0.7, 0.6, 0.6},						// red plastic
								   {0.633, 0.727811, 0.633},			// emerald
								   {0.14, 0.14, 0.14},					// slate
								   {0.0, 0.0, 0.727811},				// azul
								   {0.03, 0.03, 0.03},					// preto
								   {0.727811, 0.727811, 0.727811} };	// cinza

const GLfloat mat_shininess[] = { 27.8,		// brass
								 32.0,		// red plastic
								 76.8,		// emerald
								 18.78,		// slate
								 30.0,		// azul
								 75.0,		// preto
								 60.0 };	// cinza

enum tipo_material { brass, red_plastic, emerald, slate, azul, preto, cinza };

#ifdef __cplusplus
inline tipo_material operator++(tipo_material &rs, int)
{
	return rs = (tipo_material)(rs + 1);
}
#endif

typedef	GLdouble Vertice[3];
typedef	GLdouble Vector[4];

typedef struct POI
{
	Model_3DS	poiModel;
	GLint		id;
	char*		name;
	char*		desc;
	char*		path;
	GLfloat		x;
	GLfloat		y;
	GLfloat		z;
	GLfloat		scale;
}POI;

typedef struct teclas_t
{
	GLboolean   up, down, left, right, cam1, cam2, cam3;
}teclas_t;

typedef struct pos_t
{
	GLfloat    x, y, z;
}pos_t;

typedef struct objecto_t
{
	pos_t    pos;
	GLfloat  dir;
	GLfloat  vel;
}objecto_t;

typedef struct camera_t
{
	pos_t		eye;
	GLfloat		dir_long;  // longitude olhar (esq-dir)
	GLfloat		dir_lat;   // latitude olhar	(cima-baixo)
	GLfloat		fov;
	GLfloat		vista;
}camera_t;

typedef struct fog
{
	GLboolean   visivel;
	GLfloat		densidade;
}nevoeio;

typedef struct Estado
{
	nevoeio		fog;
	camera_t	camera;
	GLint		timer;
	teclas_t	teclas;
	GLint		mainWindow;
	int			xMouse, yMouse;
	GLboolean	light;
	GLboolean	apresentaNormais;
	GLint		lightViewer;
	ALboolean	som_foot;
	ALuint		buffer_foot;
	ALuint		source_foot;

	ALboolean	som_music;
	ALuint		buffer_music;
	ALuint		source_music;

	GLint		nodeIdentity;
	GLint		wayIdentity;
	GLint		poiIdentity;

	//GLint		eixoTranslaccao;
	//GLdouble	eixo[3];
}Estado;

typedef struct Modelo
{
#ifdef __cplusplus
	tipo_material	cor_cubo;
#else
	enum tipo_material cor_cubo;
#endif

	GLfloat			g_pos_luz1[4];
	GLfloat			g_pos_luz2[4];

	GLfloat			escala;
	GLUquadric		*quad;

	GLint			dimensao;
	GLint			xCenter;
	GLint			yCenter;

	UINT			g_Textures[NUM_TEXTURES];

	StudioModel		visitor, fountain;
	POI				poi[MAX_POIS];
	vector<No>		routeNodes;
	vector<Arco>	routeWays;
	vector<No>		visitedNodes;

	objecto_t		objecto;
	GLboolean		andar;
	GLuint			prev;
	int				duracao;

	GLuint			xMouse;
	GLuint			yMouse;
}Modelo;

// Flags
int flagFinal = 0;		//flag para controlar o som do último checkpoint
int guestMode = true;  //flag guest/usermode

//POI pois[MAX_POIS];
int numPOIs = 0;

Estado estado;
Modelo modelo;

restClient rest = restClient("http://10.8.11.147");

// Função responsável pelo carregamento do ficheiro .bmp da textura
AUX_RGBImageRec *LoadBMP(char *Filename) // Loads A Bitmap Image
{
	FILE *File = NULL; // File Handle

	if (!Filename) // Make Sure A Filename Was Given  
	{
		return NULL; // If Not Return NULL
	}

	File = fopen(Filename, "r"); // Check To See If The File Exists

	if (File) // Does The File Exist?
	{
		fclose(File); // Close The Handle
		return auxDIBImageLoad(Filename); // Load The Bitmap And Return A Pointer
	}
	return NULL; // If Load Failed Return NULL
}

// Função responsável pelo carregamento da textura para a memória
int LoadAllTextures() // Load Bitmaps And Convert To Textures
{
	int Status = FALSE; // Status Indicator

	AUX_RGBImageRec *TextureImage[NUM_TEXTURES]; // Create Storage Space For The Texture

	memset(TextureImage, 0, sizeof(void *) * 1); // Set The Pointer To NULL
	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);

	// Load The Bitmap, Check For Errors, If Bitmap's Not Found Quit
	if (TextureImage[0] = LoadBMP(FLOOR_TEXTURE))
	{
		Status = TRUE;
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_FLOOR_TEX]);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_NEAREST);
		gluBuild2DMipmaps(GL_TEXTURE_2D, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
	}

	if (TextureImage[0] = LoadBMP(ROAD_TEXTURE))
	{
		Status = TRUE;
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_ROAD_TEX]);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_NEAREST);
		gluBuild2DMipmaps(GL_TEXTURE_2D, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
	}

	if (TextureImage[0] = LoadBMP(NODE_TEXTURE))
	{
		Status = TRUE;
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_NODE_TEX]);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_NEAREST);
		gluBuild2DMipmaps(GL_TEXTURE_2D, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
	}

	if (TextureImage[0] = LoadBMP(FRONT_TEXTURE))
	{
		Status = TRUE;
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_FRONT]);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		gluBuild2DMipmaps(GL_TEXTURE_2D, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
	}

	if (TextureImage[0] = LoadBMP(RIGHT_TEXTURE))
	{
		Status = TRUE;
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_RIGHT]);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		gluBuild2DMipmaps(GL_TEXTURE_2D, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
	}

	if (TextureImage[0] = LoadBMP(BACK_TEXTURE))
	{
		Status = TRUE;
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_BACK]);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		gluBuild2DMipmaps(GL_TEXTURE_2D, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
	}

	if (TextureImage[0] = LoadBMP(LEFT_TEXTURE))
	{
		Status = TRUE;
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_LEFT]);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		gluBuild2DMipmaps(GL_TEXTURE_2D, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
	}

	if (TextureImage[0] = LoadBMP(TOP_TEXTURE))
	{
		Status = TRUE;
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_TOP]);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		gluBuild2DMipmaps(GL_TEXTURE_2D, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
	}

	if (TextureImage[0]) // If Texture Exists
	{
		if (TextureImage[0]->data) // If Texture Image Exists
		{
			free(TextureImage[0]->data); // Free The Texture Image Memory
		}

		free(TextureImage[0]); // Free The Image Structure
	}

	glBindTexture(GL_TEXTURE_2D, 0);  // desligar a textura
	return Status; // Return The Status
}

// Iniciação das variaveis de estado
void initEstado()
{
	estado.camera.eye.x = 0;
	estado.camera.eye.y = OBJECTO_ALTURA * 2;
	estado.camera.eye.z = 0;
	estado.camera.dir_long = 0;
	estado.camera.dir_lat = 0;
	estado.camera.fov = 60;
	estado.camera.vista = 0;
	estado.light = GL_FALSE;
	estado.apresentaNormais = GL_FALSE;
	estado.lightViewer = 1;
	estado.timer = 100;
	estado.fog.visivel = GL_FALSE;
	estado.fog.densidade = 0.05;

	// Inicializacao de teclas para movimentacao do visitante
	estado.teclas.up = estado.teclas.down = estado.teclas.right = estado.teclas.left = GL_FALSE;

	// Inicializacao de teclas para controlar a camara
	estado.teclas.cam1 = estado.teclas.cam2 = estado.teclas.cam3 = GL_FALSE;
}

// Iniciação das variaveis de modelo
void initModelo()
{
	modelo.escala = 0.2;
	modelo.cor_cubo = brass;
	modelo.dimensao = 0;
	modelo.xCenter = 0;
	modelo.yCenter = 0;

	modelo.objecto.pos.x = nos[9].x * MODEL_SCALE;
	modelo.objecto.pos.y = nos[9].y * MODEL_SCALE;
	modelo.objecto.pos.z = nos[9].z * MODEL_SCALE + OBJECTO_ALTURA;
	modelo.objecto.dir = 0;
	modelo.objecto.vel = OBJECTO_VELOCIDADE;
	modelo.andar = GL_FALSE;
	modelo.duracao = 0;
	modelo.xMouse = modelo.yMouse = -1;

	modelo.g_pos_luz1[0] = -5.0;
	modelo.g_pos_luz1[1] = 5.0;
	modelo.g_pos_luz1[2] = 5.0;
	modelo.g_pos_luz1[3] = 0.0;
	modelo.g_pos_luz2[0] = 5.0;
	modelo.g_pos_luz2[1] = -15.0;
	modelo.g_pos_luz2[2] = 5.0;
	modelo.g_pos_luz2[3] = 0.0;
}

// Carrega a rota recebida para o vector modelo.routeNodes[n]
void LoadRoute()
{
	modelo.routeNodes.push_back(nos[9]);
	modelo.routeNodes.push_back(nos[97]);
	modelo.routeNodes.push_back(nos[22]);
	modelo.routeNodes.push_back(nos[546]);
}

// Carrega os modelos recebidos para o vector modelo.poi[n]
void Load3dModels()
{
	if (guestMode)
	{
		modelo.poi[numPOIs].id = 1;
		modelo.poi[numPOIs].path = "Models/casa_da_musica/casa_da_musica.3ds";
		modelo.poi[numPOIs].name = "Casa da Musica";
		modelo.poi[numPOIs].desc = "Casa da musica";
		modelo.poi[numPOIs].x = 3900.0;
		modelo.poi[numPOIs].y = -10.0;
		modelo.poi[numPOIs].z = 0;
		modelo.poi[numPOIs].scale = 1.0;
		numPOIs++;

		modelo.poi[numPOIs].id = 2;
		modelo.poi[numPOIs].path = "Models/estadio_dragao/estadio_dragao.3ds";
		modelo.poi[numPOIs].name = "Estadio do Dragao";
		modelo.poi[numPOIs].desc = "Casa do FC Porto";
		modelo.poi[numPOIs].x = 3960.0;
		modelo.poi[numPOIs].y = 0.0;
		modelo.poi[numPOIs].z = 0;
		modelo.poi[numPOIs].scale = 0.5;
		numPOIs++;

		modelo.poi[numPOIs].id = 3;
		modelo.poi[numPOIs].path = "Models/torre_clerigos/torre_clerigos.3ds";
		modelo.poi[numPOIs].name = "Torre dos Clerigos";
		modelo.poi[numPOIs].desc = "Torre dos Clerigos";
		modelo.poi[numPOIs].x = 3995.0;
		modelo.poi[numPOIs].y = -15.0;
		modelo.poi[numPOIs].z = 0;
		modelo.poi[numPOIs].scale = 1.0;
		numPOIs++;
	}
}

// Iniciação dos modelos .mdl e 3DS
void initModelos3D()
{
	mdlviewer_init(VISITOR_MODEL, modelo.visitor);
	mdlviewer_init(FOUNTAIN_MODEL, modelo.fountain);

	Load3dModels();
	for (int i = 0; i < numPOIs; i++)
	{
		glPushName(i);
		modelo.poi[i].poiModel.Load(modelo.poi[i].path);
		glPopName();
	}
}

// Calcula as dimensões do modelo para ajustar o chao e a skybox aos dados recebidos.
void CalculateModelVariables()
{
	double xMin = nos[0].x, xMax = nos[0].x, yMin = nos[0].y, yMax = nos[0].y, xSum = 0, ySum = 0;

	for (int i = 1; i < numNos; i++)
	{
		if (nos[i].x < xMin)
			xMin = nos[i].x * MODEL_SCALE;

		if (nos[i].x > xMax)
			xMax = nos[i].y * MODEL_SCALE;

		if (nos[i].y < yMin)
			yMin = nos[i].y * MODEL_SCALE;

		if (nos[i].y > yMax)
			yMax = nos[i].y * MODEL_SCALE;

		xSum += nos[i].x * MODEL_SCALE;
		ySum += nos[i].y * MODEL_SCALE;
	}
	int dif_x = abs((int)xMax - xMin);
	int dif_y = abs((int)yMax - yMin);
	modelo.xCenter = xSum / numNos;
	modelo.yCenter = ySum / numNos;

	if (dif_x > dif_y)
		modelo.dimensao = dif_x / 2 + FLOOR_GAP;
	else
		modelo.dimensao = dif_y / 2 + FLOOR_GAP;

	cout << "DIMENSOES DO MODELO:" << endl;
	cout << "dif_x:\t" << dif_x << "\tdif_y: " << dif_y << endl <<
		"xCenter: " << modelo.xCenter << "\tyCenter: " << modelo.yCenter << endl <<
		"Dimensao chao: " << modelo.dimensao << endl << endl;
}

// Inicia o mapeamento do plano 2D para integrar com o ViewPort
void glEnable2D()
{
	GLint vPort[4];

	glMatrixMode(GL_PROJECTION);
	glPushMatrix();
	glLoadIdentity();

	glGetIntegerv(GL_VIEWPORT, vPort);

	glOrtho(0, vPort[2], 0, vPort[3], -1, 1);
	glMatrixMode(GL_MODELVIEW);
	glPushMatrix();
	glLoadIdentity();
}

// Termina o mapeamento do plano 2D
void glDisable2D()
{
	glMatrixMode(GL_PROJECTION);
	glPopMatrix();
	glMatrixMode(GL_MODELVIEW);
	glPopMatrix();
}

//Som dos passos 
void InitAudio(void) {
	estado.buffer_foot = alutCreateBufferFromFile("audio\\Footsteps.wav");
	alGenSources(1, &estado.source_foot);
	alSourcei(estado.source_foot, AL_BUFFER, estado.buffer_foot);
	estado.som_foot = AL_FALSE;
	//estado.som_foot = AL_TRUE;
}

//musica de fundo 
void InitAudioMusic(void) {
	estado.buffer_music = alutCreateBufferFromFile("audio\\porto.wav");
	alGenSources(1, &estado.source_music);
	alSourcei(estado.source_music, AL_BUFFER, estado.buffer_music);
	//estado.som_music = AL_FALSE;
	estado.som_music = AL_TRUE;
}

// SONS
void audio(const char * filename) {
	ALuint helloBuffer, helloSource;
	helloBuffer = alutCreateBufferFromFile(filename);
	alGenSources(1, &helloSource);
	alSourcei(helloSource, AL_BUFFER, helloBuffer);
	alSourcePlay(helloSource);
}
// Desenha a caixa do tooltip
void DrawToolTipBox(int pos)
{
	glColor3f(0.0f, 0.0f, 1.0f);
	glEnable(GL_BLEND);
	glBlendFunc(GL_ONE, GL_ONE);
	glBegin(GL_QUADS);
	glVertex2f(0, 0);
	glVertex2f(0, WINDOW_GAP * pos);
	glVertex2f(glutGet(GLUT_WINDOW_WIDTH) * 0.5, WINDOW_GAP * pos);
	glVertex2f(glutGet(GLUT_WINDOW_WIDTH) * 0.5, 0);
	glEnd();
	glDisable(GL_BLEND);
}

// Escreve texto no plano 2D
void PrintText(string text, int pos, int gap)
{
	glRasterPos2i(WINDOW_GAP + gap, WINDOW_GAP * pos);
	void * font = GLUT_BITMAP_9_BY_15;
	for (std::string::iterator i = text.begin(); i != text.end(); ++i)
	{
		char c = *i;
		glutBitmapCharacter(font, c);
	}
}

// Calcula distancia total do percurso
float getRouteTotalDistance(int routeVisited, int routeLength)
{
	float totalDist = 0;
	if (routeVisited == routeLength)
		return totalDist;
	else
	{
		for (int i = routeVisited - 1; i < routeLength - 1; i++)
		{
			No iNode = modelo.routeNodes[i];
			No fNode = modelo.routeNodes[i + 1];
			totalDist += sqrt(pow(fNode.x * MODEL_SCALE - iNode.x * MODEL_SCALE, 2) + pow(fNode.y * MODEL_SCALE - iNode.y * MODEL_SCALE, 2));
		}
	}
	return totalDist;
}

// Calcula distancia do objeto ao proximo nó
float getDistToNextNode(objecto_t obj, int routeVisited, int routeLength)
{
	float distToNextNode = 0;
	if (routeVisited == routeLength)
		return distToNextNode;
	else
	{
		No nextNode = modelo.routeNodes[routeVisited];
		distToNextNode = sqrt(pow(nextNode.x * MODEL_SCALE - obj.pos.x, 2) + pow(nextNode.y * MODEL_SCALE - obj.pos.y, 2));
	}
	return distToNextNode;
}

// Desenha a barra de progresso
void DrawTooltipProgressBar(int count, int routeLength)
{
	int gap = (glutGet(GLUT_WINDOW_WIDTH) * 0.5 - WINDOW_GAP * 2) / (routeLength - 1);
	for (int i = 0; i < routeLength - 1; i++)
	{
		if (count > 0)
		{
			glColor3f(1.0f, 0.0f, 0.0f);
			count--;
		}

		if (count == 0)
			glColor3f(0.0f, 1.0f, 0.0f);

		glBegin(GL_QUADS);
		glVertex2f(WINDOW_GAP + i * gap, WINDOW_GAP - 5);
		glVertex2f(WINDOW_GAP + i * gap, WINDOW_GAP * 2.0 - 5);
		glVertex2f(WINDOW_GAP + gap + (i * gap), WINDOW_GAP * 2.0 - 5);
		glVertex2f(WINDOW_GAP + gap + (i * gap), WINDOW_GAP - 5);
		glEnd();
	}
}

// Funcao geral para desenhar o tooltip
void Tooltip(objecto_t obj)
{
	glEnable2D();

	int pos = 6;
	int routeVisited = modelo.visitedNodes.size();
	int routeLength = modelo.routeNodes.size();
	int percentage = 0;
	if ((routeVisited - 1) != 0)
		percentage = ((routeVisited - 1) * 100) / (routeLength - 1);

	DrawToolTipBox(pos);
	DrawTooltipProgressBar(routeVisited, routeLength);
	glColor3f(1.0f, 1.0f, 1.0f);

	string text = "x: " + to_string(obj.pos.x) + ", y: " + to_string(obj.pos.y);
	PrintText(text, pos - 1, 0);

	text = "Dist. parcial: " + to_string((int)getDistToNextNode(obj, routeVisited, routeLength)) +
		"m | Dist.total: " + to_string((int)getRouteTotalDistance(routeVisited, routeLength)) + "m";
	PrintText(text, pos - 2, 0);


	//som para final
	if ((int)getRouteTotalDistance(routeVisited, routeLength) < 10) {

		if (flagFinal == 0) {
			audio("audio/stereo.wav");
			flagFinal = 1;
		}
	}

	PrintText("Barra de progresso", pos - 4, (glutGet(GLUT_WINDOW_WIDTH) / 7.0));
	text = to_string(percentage) + "%";
	PrintText(text, pos - 5, (glutGet(GLUT_WINDOW_WIDTH) * 0.5 - WINDOW_GAP * 2) * 0.5);
	glDisable2D();
}

// Funcao responsavel pelo cálculo das colisoes
GLboolean detectaColisao(GLfloat nx, GLfloat nz)
{

	return GL_FALSE;
}

void Timer(int value)
{

	ALint state;

	GLfloat nx = 0, ny = 0;
	GLboolean andar = GL_FALSE;

	GLuint curr = glutGet(GLUT_ELAPSED_TIME);

	GLuint flagCounter = 0; // contador para atualizar mapa. atualiza quando contador = 5;

	//Som dos passos
	alGetSourcei(estado.source_foot, AL_SOURCE_STATE, &state);
	if (estado.som_foot)
	{
		if (state != AL_PLAYING)
			alSourcePlay(estado.source_foot);
	}
	else {
		if (state == AL_PLAYING)
			alSourceStop(estado.source_foot);
	}

	//musica de fundo
	alGetSourcei(estado.source_music, AL_SOURCE_STATE, &state);
	if (estado.som_music)
	{
		if (state != AL_PLAYING)
			alSourcePlay(estado.source_music);
	}
	else {
		if (state == AL_PLAYING)
			alSourceStop(estado.source_music);
	}

	// calcula velocidade baseado no tempo passado
	float velocidade = modelo.objecto.vel * (curr - modelo.prev) * 0.001;

	glutTimerFunc(estado.timer, Timer, 0);
	if (modelo.duracao > 0)
	{
		modelo.duracao -= (int)curr - (int)modelo.prev;
		modelo.prev = curr;
		glutPostRedisplay();
		return;
	}

	modelo.prev = curr;

	if (estado.teclas.up) {
		flagCounter++;
		// calcula nova posição nx,ny
		nx = modelo.objecto.pos.x + velocidade * cos(-modelo.objecto.dir);
		ny = modelo.objecto.pos.y + velocidade * sin(-modelo.objecto.dir);
		if (!detectaColisao(nx + OBJECTO_RAIO * cos(-modelo.objecto.dir), ny + OBJECTO_RAIO * sin(-modelo.objecto.dir))
			&& !detectaColisao(nx + OBJECTO_RAIO * cos(-modelo.objecto.dir - rad(45)), ny + OBJECTO_RAIO * sin(-modelo.objecto.dir - rad(45)))
			&& !detectaColisao(nx + OBJECTO_RAIO * cos(-modelo.objecto.dir + rad(45)), ny + OBJECTO_RAIO * sin(-modelo.objecto.dir + rad(45))))
		{
			modelo.objecto.pos.x = nx;
			modelo.objecto.pos.y = ny;
			if (flagCounter == 5) {
				// codigo para atualizar cenario atraves da posicao do objeto
				flagCounter = 0;
			}
		}
		andar = GL_TRUE;
	}

	if (estado.teclas.down) {
		flagCounter++;
		// calcula nova posição nx,ny
		nx = modelo.objecto.pos.x - velocidade * cos(-modelo.objecto.dir);
		ny = modelo.objecto.pos.y - velocidade * sin(-modelo.objecto.dir);
		if (!detectaColisao(nx - OBJECTO_RAIO * cos(-modelo.objecto.dir), ny - OBJECTO_RAIO * sin(-modelo.objecto.dir))
			&& !detectaColisao(nx - OBJECTO_RAIO * cos(-modelo.objecto.dir - rad(45)), ny - OBJECTO_RAIO * sin(-modelo.objecto.dir - rad(45)))
			&& !detectaColisao(nx - OBJECTO_RAIO * cos(-modelo.objecto.dir + rad(45)), ny - OBJECTO_RAIO * sin(-modelo.objecto.dir + rad(45))))
		{
			modelo.objecto.pos.x = nx;
			modelo.objecto.pos.y = ny;
			if (flagCounter == 5) {
				// codigo para atualizar cenario atraves da posicao do objeto
				flagCounter = 0;
			}
		}
		andar = GL_TRUE;
	}
	if (estado.teclas.left) {
		modelo.objecto.dir -= rad(OBJECTO_ROTACAO);
		estado.camera.dir_long -= rad(OBJECTO_ROTACAO);
	}
	if (estado.teclas.right) {
		modelo.objecto.dir += rad(OBJECTO_ROTACAO);
		estado.camera.dir_long += rad(OBJECTO_ROTACAO);
	}

	if (andar)
	{
		if (modelo.visitor.GetSequence() != 3 && modelo.visitor.GetSequence() != 20)
		{
			estado.som_foot = AL_TRUE;

			modelo.visitor.SetSequence(3);
			modelo.visitor.SetSequence(3);
		}
	}
	else
	{
		if (modelo.visitor.GetSequence() != 0)
		{
			estado.som_foot = AL_FALSE;
			modelo.visitor.SetSequence(0);
			modelo.visitor.SetSequence(0);
		}
	}

	//Configuração das vistas das camaras
	if (estado.teclas.cam1)
		estado.camera.vista = 0;
	if (estado.teclas.cam2)
		estado.camera.vista = 1;
	if (estado.teclas.cam3)
		estado.camera.vista = 2;

	glutPostRedisplay();
}

void imprime_ajuda(void)
{
	printf("\n\nDesenho da cidade do Porto \n");
	printf("h,H - Ajuda \n");
	printf("i,I - Reset dos Valores \n");

	printf("******* Movimentar o modelo ******* \n");
	printf("UP    - Movimenta modelo para a frente \n");
	printf("DOWN  - Movimenta modelo para a tras \n");
	printf("RIGHT - Roda modelo para a direita \n");
	printf("LEFT  - Roda modelo para a esquerda \n");

	printf("******* Camera ******* \n");
	printf("F1  - Vista na 3ª pessoa \n");
	printf("F2  - Vista na 1ª pessoa \n");
	printf("F3  - Vista de topo \n");

	printf("******* Diversos ******* \n");
	printf("l,L - Alterna o calculo luz entre Z e eye (GL_LIGHT_MODEL_LOCAL_VIEWER)\n");
	printf("k,K - Alerna luz de camera com luz global \n");
	printf("s,S - PolygonMode Fill \n");
	printf("w,W - PolygonMode Wireframe \n");
	printf("p,P - PolygonMode Point \n");
	printf("c,C - Liga/Desliga Cull Face \n");
	printf("n,N - Liga/Desliga apresentação das normais \n");
	printf("a,A - Carregar dados da API \n");
	printf("m,M - Liga/Desliga música ambiente \n");
	printf("f,F - Liga/Desliga nevoeiro \n");

	printf("******* Opções desativadas ******* \n");
	printf("Botão esquerdo - Arrastar os eixos (centro da camera)\n");
	printf("Botão direito  - Rodar camera\n");
	printf("Botão direito com CTRL - Zoom-in/out\n");
	printf("PAGE_UP, PAGE_DOWN - Altera distância da camara \n");
	printf("ESC - Sair\n");
}


void material(enum tipo_material mat)
{
	glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, mat_ambient[mat]);
	glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, mat_diffuse[mat]);
	glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, mat_specular[mat]);
	glMaterialf(GL_FRONT_AND_BACK, GL_SHININESS, mat_shininess[mat]);
}

const GLfloat red_light[] = { 1.0, 0.0, 0.0, 1.0 };
const GLfloat green_light[] = { 0.0, 1.0, 0.0, 1.0 };
const GLfloat blue_light[] = { 0.0, 0.0, 1.0, 1.0 };
const GLfloat white_light[] = { 1.0, 1.0, 1.0, 1.0 };


void putLights(GLfloat* diffuse)
{
	const GLfloat white_amb[] = { 0.7, 0.7, 0.7, 1.0 };

	glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuse);
	glLightfv(GL_LIGHT0, GL_SPECULAR, white_light);
	glLightfv(GL_LIGHT0, GL_AMBIENT, white_amb);
	glLightfv(GL_LIGHT0, GL_POSITION, modelo.g_pos_luz1);

	glLightfv(GL_LIGHT1, GL_DIFFUSE, diffuse);
	glLightfv(GL_LIGHT1, GL_SPECULAR, white_light);
	glLightfv(GL_LIGHT1, GL_AMBIENT, white_amb);
	glLightfv(GL_LIGHT1, GL_POSITION, modelo.g_pos_luz2);

	/* desenhar luz */
	//material(red_plastic);
	//glPushMatrix();
	//	glTranslatef(modelo.g_pos_luz1[0], modelo.g_pos_luz1[1], modelo.g_pos_luz1[2]);
	//	glDisable(GL_LIGHTING);
	//	glColor3f(1.0, 1.0, 1.0);
	//	glutSolidCube(0.1);
	//	glEnable(GL_LIGHTING);
	//glPopMatrix();
	//glPushMatrix();
	//	glTranslatef(modelo.g_pos_luz2[0], modelo.g_pos_luz2[1], modelo.g_pos_luz2[2]);
	//	glDisable(GL_LIGHTING);
	//	glColor3f(1.0, 1.0, 1.0);
	//	glutSolidCube(0.1);
	//	glEnable(GL_LIGHTING);
	//glPopMatrix();

	glEnable(GL_LIGHT0);
	glEnable(GL_LIGHT1);
}


UINT CreateOpenGL3DFont(LPSTR strFontName, float extrude)
{
	UINT	fontListID = 0;
	HFONT	hFont;

	fontListID = glGenLists(MAX_CHARS);
	hFont = CreateFont(0, 0, 0, 0, FW_BOLD, FALSE, FALSE, FALSE, ANSI_CHARSET, OUT_TT_PRECIS, CLIP_DEFAULT_PRECIS,
		ANTIALIASED_QUALITY, FF_DONTCARE | DEFAULT_PITCH, strFontName);

	hOldFont = (HFONT)SelectObject(g_hDC, hFont);

	wglUseFontOutlines(g_hDC, 0, MAX_CHARS - 1, fontListID, 0, extrude, WGL_FONT_POLYGONS, g_GlyphInfo);
	return fontListID;
}

void Draw3DText(const char *strString, ...)
{
	char		strText[256];
	va_list		argumentPtr;
	float		unitLength = 0.0f;

	if (strString == NULL)
		return;

	va_start(argumentPtr, strString);
	vsprintf(strText, strString, argumentPtr);
	va_end(argumentPtr);

	for (int i = 0; i < (int)strlen(strText); i++)
		unitLength += g_GlyphInfo[strText[i]].gmfCellIncX;

	glTranslatef(0.0f - (unitLength / 2), 0.0f, 0.0f);

	glPushAttrib(GL_LIST_BIT);
	glListBase(g_FontListID);
	glCallLists((int)strlen(strText), GL_UNSIGNED_BYTE, strText);
	glPopAttrib();
}

// Desenha seta para informar do sentido do proximo nó
void DrawArrow()
{
	gluCylinder(modelo.quad, 0.25, 0.25, 3, 16, 15);
	glTranslatef(0, 0, 3);
	glPushMatrix();
	{
		glRotatef(180, 0, 1, 0);
		gluDisk(modelo.quad, 0.5, 1, 16, 6);
	}
	glPopMatrix();
	gluCylinder(modelo.quad, 1, 0, 2, 16, 15);
}

// Desenha rota do percurso carregado
void DrawRoute()
{
	glDisable(GL_LIGHTING);
	glColor3f(0.0f, 1.0f, 0.0f);
	No node, nextNode;
	int length = modelo.routeNodes.size();

	for (int i = 0; i < length - 1; i++)
	{
		node = modelo.routeNodes[i];
		nextNode = modelo.routeNodes[i + 1];
		glPushMatrix();
		{
			glTranslatef(node.x * MODEL_SCALE, node.y * MODEL_SCALE, node.z + ROUTE_HEIGHT * MODEL_SCALE);
			Draw3DText("WAYPOINT #" + i);

			glRotatef(90, 0, 1, 0);
			float slope = (nextNode.y - node.y) / (nextNode.x - node.x);
			float angle = graus(slope);

			if (angle < -180)
				angle += 180;
			if (angle > 180)
				angle -= 180;

			if (node.x < nextNode.x)
			{
				if (node.y > nextNode.y)
					glRotatef(-angle, 1, 0, 0);
				else
					glRotatef(angle, 1, 0, 0);
			}

			if (node.x > nextNode.x)
			{
				if (node.y < nextNode.y)
					glRotatef(180 - angle, 1, 0, 0);
				else
					glRotatef(180 + angle, 1, 0, 0);
			}

			DrawArrow();
		}
		glPopMatrix();
	}

	node = modelo.routeNodes[length - 1];
	glPushMatrix();
	{
		glTranslatef(node.x * MODEL_SCALE, node.y * MODEL_SCALE, node.z + ROUTE_HEIGHT * MODEL_SCALE);
		Draw3DText("Finish");
		glutSolidSphere(0.5, 12, 12);
	}
	glPopMatrix();

	glColor3f(1.0f, 1.0f, 1.0f);
	glEnable(GL_LIGHTING);
}

// Atualiza vector dos nós visitados
void UpdateVisitedNodes(objecto_t obj)
{
	int routeLength = modelo.routeNodes.size();
	int visitedNodesLength = modelo.visitedNodes.size();

	//cout << "Nos Visitados: " << visitedNodesLength << endl;

	if (routeLength == visitedNodesLength)
		return;

	if (modelo.visitedNodes.size() == 0)
	{
		No node = modelo.routeNodes[0];
		float dist = sqrt(pow(node.x * MODEL_SCALE - obj.pos.x, 2) + pow(node.y * MODEL_SCALE - obj.pos.y, 2));
		if (dist < node.largura * MODEL_SCALE)
			modelo.visitedNodes.push_back(node);
	}
	else
	{
		No lastNodeVisited = modelo.visitedNodes.back();
		int pos = 0;

		for (int i = 0; i < routeLength; i++)
			if (lastNodeVisited.id == modelo.routeNodes[i].id)
				pos = i;

		No nextNode = modelo.routeNodes[pos + 1];
		float dist = sqrt(pow(nextNode.x * MODEL_SCALE - obj.pos.x, 2) + pow(nextNode.y * MODEL_SCALE - obj.pos.y, 2));
		if (dist < nextNode.largura * MODEL_SCALE) {
			modelo.visitedNodes.push_back(nextNode);
			audio("audio/doh2.wav"); //som para passagem em checkpoint
		}
	}
}

void desenhaChao()
{
	int xi = modelo.xCenter - modelo.dimensao;
	int xf = modelo.xCenter + modelo.dimensao;
	int yi = modelo.yCenter - modelo.dimensao;
	int yf = modelo.yCenter + modelo.dimensao;

	if (xf < xi)
	{
		int tmp = xi;
		xi = xf;
		xf = tmp;
	}
	if (yf < yi)
	{
		int tmp = yi;
		yi = yf;
		yf = tmp;
	}
	//cout << "xi: " << xi << "\txf: " << xf << "\tyi: " << yi << "\tyf: " << yf << endl;

	glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_FLOOR_TEX]);

	for (int i = xi; i < xf; i += FLOOR_STEP)
		for (int j = yi; j < yf; j += FLOOR_STEP)
		{
			glBegin(GL_QUADS);
			glNormal3f(0, 0, 1);
			glTexCoord2f(0.0f, 0.0f); glVertex2f(i, j);
			glTexCoord2f(1.0f, 0.0f); glVertex2f(i + FLOOR_STEP, j);
			glTexCoord2f(1.0f, 1.0f); glVertex2f(i + FLOOR_STEP, j + FLOOR_STEP);
			glTexCoord2f(0.0f, 1.0f); glVertex2f(i, j + FLOOR_STEP);
			glEnd();
		}

	glBindTexture(GL_TEXTURE_2D, NULL);
}

void CrossProduct(GLdouble v1[], GLdouble v2[], GLdouble cross[])
{
	cross[0] = v1[1] * v2[2] - v1[2] * v2[1];
	cross[1] = v1[2] * v2[0] - v1[0] * v2[2];
	cross[2] = v1[0] * v2[1] - v1[1] * v2[0];
}

GLdouble VectorNormalize(GLdouble v[])
{
	int	i;
	GLdouble length;

	if (fabs(v[1] - 0.000215956) < 0.0001)
		i = 1;

	length = 0;
	for (i = 0; i < 3; i++)
		length += v[i] * v[i];
	length = sqrt(length);
	if (length == 0)
		return 0;

	for (i = 0; i < 3; i++)
		v[i] /= length;

	return length;
}

void desenhaNormal(GLdouble x, GLdouble y, GLdouble z, GLdouble normal[], tipo_material mat) {

	switch (mat) {
	case red_plastic:
		glColor3f(1, 0, 0);
		break;
	case azul:
		glColor3f(0, 0, 1);
		break;
	case emerald:
		glColor3f(0, 1, 0);
		break;
	default:
		glColor3f(1, 1, 0);
	}
	glDisable(GL_LIGHTING);
	glPushMatrix();
	glTranslated(x, y, z);
	glScaled(0.4, 0.4, 0.4);
	glBegin(GL_LINES);
	glVertex3d(0, 0, 0);
	glVertex3dv(normal);
	glEnd();
	glPopMatrix();
	glEnable(GL_LIGHTING);
}

void desenhaCaminho(GLfloat xi, GLfloat yi, GLfloat zi, GLfloat xf, GLfloat yf, GLfloat zf, int orient)
{
	GLdouble v1[3], v2[3], cross[3];
	GLdouble length;
	v1[0] = xf - xi;
	v1[1] = 0;
	v2[0] = 0;
	v2[1] = yf - yi;

	switch (orient) {
	case NORTE_SUL:
		v1[2] = 0;
		v2[2] = zf - zi;
		CrossProduct(v1, v2, cross);
		//printf("cross x=%lf y=%lf z=%lf",cross[0],cross[1],cross[2]);
		length = VectorNormalize(cross);
		//printf("Normal x=%lf y=%lf z=%lf length=%lf\n",cross[0],cross[1],cross[2]);

		material(red_plastic);
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_ROAD_TEX]);
		glPushMatrix();
		{
			glDisable(GL_LIGHTING);
			glBegin(GL_QUADS);
			glNormal3dv(cross);
			//glNormal3f(0, 0, 1);
			glTexCoord2f(0.0f, 0.0f); glVertex3f(xi, yi, zi);
			glTexCoord2f(1.0f, 0.0f); glVertex3f(xf, yi, zi);
			glTexCoord2f(1.0f, 1.0f); glVertex3f(xf, yf, zf);
			glTexCoord2f(0.0f, 1.0f); glVertex3f(xi, yf, zf);
			glEnd();
			glEnable(GL_LIGHTING);
		}
		glPopMatrix();
		glBindTexture(GL_TEXTURE_2D, NULL);

		if (estado.apresentaNormais) {
			desenhaNormal(xi, yi, zi, cross, red_plastic);
			desenhaNormal(xf, yi, zi, cross, red_plastic);
			desenhaNormal(xf, yf, zf, cross, red_plastic);
			desenhaNormal(xi, yi, zf, cross, red_plastic);
		}
		break;
	case ESTE_OESTE:
		v1[2] = zf - zi;
		v2[2] = 0;
		CrossProduct(v1, v2, cross);
		//printf("cross x=%lf y=%lf z=%lf",cross[0],cross[1],cross[2]);
		length = VectorNormalize(cross);
		//printf("Normal x=%lf y=%lf z=%lf length=%lf\n",cross[0],cross[1],cross[2]);

		material(red_plastic);
		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_ROAD_TEX]);
		glPushMatrix();
		{
			glDisable(GL_LIGHTING);
			glBegin(GL_QUADS);
			//glNormal3f(0, 0, 1);
			glNormal3dv(cross);
			glTexCoord2f(0.0f, 0.0f); glVertex3f(xi, yi, zi);
			glTexCoord2f(0.0f, 1.0f); glVertex3f(xf, yi, zf);
			glTexCoord2f(1.0f, 1.0f); glVertex3f(xf, yf, zf);
			glTexCoord2f(1.0f, 0.0f); glVertex3f(xi, yf, zi);
			glEnd();
		}
		glPopMatrix();
		glBindTexture(GL_TEXTURE_2D, NULL);

		if (estado.apresentaNormais) {
			desenhaNormal(xi, yi, zi, cross, red_plastic);
			desenhaNormal(xf, yi, zf, cross, red_plastic);
			desenhaNormal(xf, yf, zf, cross, red_plastic);
			desenhaNormal(xi, yi, zi, cross, red_plastic);
		}
		break;
	}
}

// Desenha os caminhos diagonais
void desenhaCaminhoDiagonal(GLfloat xi_1, GLfloat yi_1, GLfloat zi, GLfloat xf_1, GLfloat yf_1, GLfloat zf, GLfloat xf_2, GLfloat yf_2,
	GLfloat xi_2, GLfloat yi_2)
{
	GLdouble v1[3], v2[3], cross[3];
	GLdouble length;
	v1[0] = xf_1 - xi_1;
	v1[1] = 0;
	v2[0] = 0;
	v2[1] = yf_1 - yi_1;

	v1[2] = 0;
	v2[2] = zf - zi;
	CrossProduct(v1, v2, cross);
	//printf("cross x=%lf y=%lf z=%lf",cross[0],cross[1],cross[2]);
	length = VectorNormalize(cross);
	//printf("Normal x=%lf y=%lf z=%lf length=%lf\n",cross[0],cross[1],cross[2]);

	material(red_plastic);

	glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_ROAD_TEX]);
	glPushMatrix();
	{
		glDisable(GL_LIGHTING);
		glBegin(GL_QUADS);
		glNormal3dv(cross);
		//glNormal3f(0, 0, 1);
		glTexCoord2f(0.0f, 0.0f); glVertex3f(xi_1, yi_1, zi);
		glTexCoord2f(0.0f, 1.0f); glVertex3f(xf_1, yf_1, zf);
		glTexCoord2f(1.0f, 1.0f); glVertex3f(xf_2, yf_2, zf);
		glTexCoord2f(1.0f, 0.0f); glVertex3f(xi_2, yi_2, zi);
		glEnd();
		glEnable(GL_LIGHTING);
	}
	glPopMatrix();
	glBindTexture(GL_TEXTURE_2D, NULL);

	if (estado.apresentaNormais)
	{
		desenhaNormal(xi_1, yi_1, zi, cross, red_plastic);
		desenhaNormal(xf_1, yf_1, zf, cross, red_plastic);
		desenhaNormal(xf_2, yf_2, zf, cross, red_plastic);
		desenhaNormal(xi_2, yi_2, zi, cross, red_plastic);
	}
}

void desenhaNo(int no)
{
	glPushMatrix();
	{
		glDisable(GL_LIGHTING);
		glTranslatef(nos[no].x, nos[no].y, nos[no].z + 0.01);
		material(azul);

		glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_NODE_TEX]);
		gluQuadricTexture(modelo.quad, GL_TRUE);
		gluDisk(modelo.quad, 0, nos[no].largura, 20, 2);
		glBindTexture(GL_TEXTURE_2D, NULL);

		glScalef(FOUNTAIN_SCALE, FOUNTAIN_SCALE, FOUNTAIN_SCALE);
		mdlviewer_display(modelo.fountain);
		glEnable(GL_LIGHTING);
	}
	glPopMatrix();
}

void desenhaArco(Arco arco) {
	No *noi, *nof;

	// arco vertical
	if (nos[arco.noi].x == nos[arco.nof].x) {
		if (nos[arco.noi].y < nos[arco.nof].y) {
			noi = &nos[arco.noi];
			nof = &nos[arco.nof];
		}
		else {
			nof = &nos[arco.noi];
			noi = &nos[arco.nof];
		}
		desenhaCaminho(noi->x - 0.5*arco.largura, noi->y + 0.5*noi->largura, noi->z, nof->x + 0.5*arco.largura, nof->y - 0.5*nof->largura, nof->z, NORTE_SUL);
	}
	else {
		//arco horizontal
		if (nos[arco.noi].y == nos[arco.nof].y) {
			if (nos[arco.noi].x < nos[arco.nof].x) {
				noi = &nos[arco.noi];
				nof = &nos[arco.nof];
			}
			else {
				nof = &nos[arco.noi];
				noi = &nos[arco.nof];
			}
			desenhaCaminho(noi->x + 0.5*noi->largura, noi->y - 0.5*arco.largura, noi->z, nof->x - 0.5*nof->largura, nof->y + 0.5*arco.largura, nof->z, ESTE_OESTE);
		}
		else
		{
			// arco diagonal
			double dif_x = nos[arco.nof].x - nos[arco.noi].x;
			double dif_y = nos[arco.nof].y - nos[arco.noi].y;
			double slope = dif_y / dif_x;
			double slope_prep = -1 / slope;

			// coordenadas do caminho - no origem //
			double b1 = nos[arco.noi].y - slope_prep * nos[arco.noi].x;
			double coord_xi_1 = nos[arco.noi].x + sqrt(pow(arco.largura / 2, 2) / (1 + pow(slope_prep, 2)));
			double coord_xi_2 = nos[arco.noi].x - sqrt(pow(arco.largura / 2, 2) / (1 + pow(slope_prep, 2)));
			double coord_yi_1 = slope_prep * coord_xi_1 + b1;
			double coord_yi_2 = slope_prep * coord_xi_2 + b1;

			// coordenadas do caminho - no destino //
			double b2 = nos[arco.nof].y - slope_prep * nos[arco.nof].x;
			double coord_xf_1 = nos[arco.nof].x + sqrt(pow(arco.largura / 2, 2) / (1 + pow(slope_prep, 2)));
			double coord_xf_2 = nos[arco.nof].x - sqrt(pow(arco.largura / 2, 2) / (1 + pow(slope_prep, 2)));
			double coord_yf_1 = slope_prep * coord_xf_1 + b2;
			double coord_yf_2 = slope_prep * coord_xf_2 + b2;

			desenhaCaminhoDiagonal(coord_xi_1, coord_yi_1, nos[arco.noi].z, coord_xf_1, coord_yf_1, nos[arco.nof].z, coord_xf_2, coord_yf_2, coord_xi_2, coord_yi_2);
		}
	}
}

void desenhaLabirinto()
{
	glPushMatrix();
	glTranslatef(0, 0, 0.05);
	glScalef(MODEL_SCALE, MODEL_SCALE, MODEL_SCALE);
	material(red_plastic);
	for (int i = 0; i < numNos; i++)
	{
		glPushName(i);
		desenhaNo(i);
		glPopName();
	}
	material(emerald);
	for (int i = 0; i < numArcos; i++)
	{
		glPushName(i);
		desenhaArco(arcos[i]);
		glPopName();
	}
	glPopMatrix();
}

void styleFog() {
	if (estado.fog.visivel) {
		GLfloat fogColor[4] = { 0.5, 0.5, 0.5, 1.0 };
		glEnable(GL_FOG);
		glFogi(GL_FOG_MODE, GL_EXP2);
		glFogfv(GL_FOG_COLOR, fogColor);
		glFogf(GL_FOG_DENSITY, estado.fog.densidade);
		glHint(GL_FOG_HINT, GL_NICEST);
	}
	else {
		glDisable(GL_FOG);
	}
}

// Desenha a skybox
void DrawSkyBox()
{
	int xi = modelo.xCenter - modelo.dimensao;
	int xf = modelo.xCenter + modelo.dimensao;
	int yi = modelo.yCenter - modelo.dimensao;
	int yf = modelo.yCenter + modelo.dimensao;
	int z = modelo.dimensao / 2;

	glDisable(GL_LIGHTING);
	// FRONT
	glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_FRONT]);
	glBegin(GL_QUADS);
	glTexCoord2f(1.0f, 0.0f); glVertex3f(xi, yf, -z);
	glTexCoord2f(1.0f, 1.0f); glVertex3f(xi, yf, z);
	glTexCoord2f(0.0f, 1.0f); glVertex3f(xi, yi, z);
	glTexCoord2f(0.0f, 0.0f); glVertex3f(xi, yi, -z);
	glEnd();
	// RIGHT
	glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_RIGHT]);
	glBegin(GL_QUADS);
	glTexCoord2f(1.0f, 0.0f); glVertex3f(xf, yf, -z);
	glTexCoord2f(1.0f, 1.0f); glVertex3f(xf, yf, z);
	glTexCoord2f(0.0f, 1.0f); glVertex3f(xi, yf, z);
	glTexCoord2f(0.0f, 0.0f); glVertex3f(xi, yf, -z);
	glEnd();
	// BACK
	glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_BACK]);
	glBegin(GL_QUADS);
	glTexCoord2f(1.0f, 1.0f); glVertex3f(xf, yi, z);
	glTexCoord2f(1.0f, 0.0f); glVertex3f(xf, yi, -z);
	glTexCoord2f(0.0f, 0.0f); glVertex3f(xf, yf, -z);
	glTexCoord2f(0.0f, 1.0f); glVertex3f(xf, yf, z);
	glEnd();
	// LEFT
	glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_LEFT]);
	glBegin(GL_QUADS);
	glTexCoord2f(1.0f, 1.0f); glVertex3f(xi, yi, z);
	glTexCoord2f(1.0f, 0.0f); glVertex3f(xi, yi, -z);
	glTexCoord2f(0.0f, 0.0f); glVertex3f(xf, yi, -z);
	glTexCoord2f(0.0f, 1.0f); glVertex3f(xf, yi, z);
	glEnd();
	// TOP
	glBindTexture(GL_TEXTURE_2D, modelo.g_Textures[ID_SB_TOP]);
	glBegin(GL_QUADS);
	glTexCoord2f(1.0f, 1.0f); glVertex3f(xf, yf, z);
	glTexCoord2f(0.0f, 1.0f); glVertex3f(xf, yi, z);
	glTexCoord2f(0.0f, 0.0f); glVertex3f(xi, yi, z);
	glTexCoord2f(1.0f, 0.0f); glVertex3f(xi, yf, z);
	glEnd();

	glBindTexture(GL_TEXTURE_2D, NULL);
	glEnable(GL_LIGHTING);
}


void motionNavigateWindow(int x, int y)
{
	int dif;
	dif = x - (int)modelo.xMouse;
	estado.camera.dir_long -= dif * rad(EYE_ROTACAO);
	dif = y - (int)modelo.yMouse;
	estado.camera.dir_lat -= dif * rad(EYE_ROTACAO);

	// LIMITAR ROTACAO +/- 45 GRAUS
	modelo.xMouse = x;
	modelo.yMouse = y;
}


void mouseNavigateWindow(int button, int state, int x, int y)
{
	if (button == GLUT_RIGHT_BUTTON)
		if (state == GLUT_DOWN)
		{
			modelo.xMouse = x;
			modelo.yMouse = y;
			glutMotionFunc(motionNavigateWindow);
		}
		else
			glutMotionFunc(NULL);
}

void setNavigateWindowCamera(camera_t *cam, objecto_t obj)
{
	pos_t center;
	int vCamX = 0, vCamY = 0, vCamZ = 0;

	// Vista na 3ª Pessoa
	if (estado.camera.vista == 0)
	{
		center.x = obj.pos.x;
		center.y = obj.pos.y;
		center.z = obj.pos.z + OBJECTO_ALTURA;
		cam->eye.x = center.x - 5 * cos(-cam->dir_long);
		cam->eye.y = center.y - 5 * sin(-cam->dir_long);
		cam->eye.z = center.z + OBJECTO_ALTURA / 1.5;
		vCamX = 0; vCamY = 0, vCamZ = 1;
	}
	// Vista na 1ª pessoa
	if (estado.camera.vista == 1)
	{
		cam->eye.x = obj.pos.x;
		cam->eye.y = obj.pos.y;
		cam->eye.z = obj.pos.z + 0.2;
		center.x = cam->eye.x + 1 * cos(-cam->dir_long) * cos(cam->dir_lat);
		center.y = cam->eye.y + 1 * sin(-cam->dir_long) * cos(cam->dir_lat);
		center.z = cam->eye.z + 1 * sin(cam->dir_lat);
		vCamX = 0; vCamY = 0, vCamZ = 1;
	}
	// Vista de topo
	if (estado.camera.vista == 2)
	{
		int dist = 100;
		cam->eye.x = obj.pos.x;
		cam->eye.y = obj.pos.y;
		cam->eye.z = obj.pos.z + dist;
		center.x = obj.pos.x;
		center.y = obj.pos.y;
		center.z = obj.pos.z;
		vCamX = 1; vCamY = 0, vCamZ = 0;
	}

	if (estado.light)
	{
		gluLookAt(cam->eye.x, cam->eye.y, cam->eye.z, center.x, center.y, center.z, vCamX, vCamY, vCamZ);
		putLights((GLfloat*)white_light);
	}
	else
	{
		putLights((GLfloat*)white_light);
		gluLookAt(cam->eye.x, cam->eye.y, cam->eye.z, center.x, center.y, center.z, vCamX, vCamY, vCamZ);
	}
}


void display(void)
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();

	setNavigateWindowCamera(&estado.camera, modelo.objecto);
	material(slate);

	desenhaChao();
	//desenhaEixos();
	desenhaLabirinto();
	DrawRoute();
	UpdateVisitedNodes(modelo.objecto);
	DrawSkyBox();

	glDisable(GL_LIGHTING);

	// Carrega modelo visitante
	glPushMatrix();
	{
		glTranslatef(modelo.objecto.pos.x, modelo.objecto.pos.y, modelo.objecto.pos.z);
		glRotatef(graus(modelo.objecto.dir), 0, 0, -1);
		glScalef(VISITOR_SCALE, VISITOR_SCALE, VISITOR_SCALE);
		mdlviewer_display(modelo.visitor);
	}
	glPopMatrix();
	Tooltip(modelo.objecto);

	// Carrega modelos POI
	for (int i = 0; i < numPOIs; i++)
	{
		glPushMatrix();
		{
			glTranslatef(modelo.poi[i].x * MODEL_SCALE, modelo.poi[i].y * MODEL_SCALE, modelo.poi[i].z * MODEL_SCALE);
			glRotated(180, 1, 0, 0);
			glScalef(modelo.poi[i].scale, modelo.poi[i].scale, modelo.poi[i].scale);
			modelo.poi[i].poiModel.Draw();
		}
		glPopMatrix();
	}

	glEnable(GL_LIGHTING);

	styleFog();

	//if (estado.eixoTranslaccao) {
	//	// desenha plano de translacção
	//	cout << "Translate... " << estado.eixoTranslaccao << endl;
		//desenhaPlanoDrag(estado.eixoTranslaccao);
	//}

	glFlush();
	glutSwapBuffers();
}

void keyboard(unsigned char key, int x, int y)
{
	switch (key)
	{
	case 27:
		exit(0);
		break;
	case 'h':
	case 'H':
		imprime_ajuda();
		break;
	case 'l':
	case 'L':
		if (estado.lightViewer)
			estado.lightViewer = 0;
		else
			estado.lightViewer = 1;
		glLightModeli(GL_LIGHT_MODEL_LOCAL_VIEWER, estado.lightViewer);
		glutPostRedisplay();
		break;
	case 'k':
	case 'K':
		estado.light = !estado.light;
		glutPostRedisplay();
		break;
	case 'w':
	case 'W':
		glDisable(GL_LIGHTING);
		glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
		glutPostRedisplay();
		break;
	case 'p':
	case 'P':
		glDisable(GL_LIGHTING);
		glPolygonMode(GL_FRONT_AND_BACK, GL_POINT);
		glutPostRedisplay();
		break;
	case 's':
	case 'S':
		glEnable(GL_LIGHTING);
		glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
		glutPostRedisplay();
		break;
	case 'c':
	case 'C':
		if (glIsEnabled(GL_CULL_FACE))
			glDisable(GL_CULL_FACE);
		else
			glEnable(GL_CULL_FACE);
		glutPostRedisplay();
		break;
	case 'n':
	case 'N':
		estado.apresentaNormais = !estado.apresentaNormais;
		glutPostRedisplay();
		break;
	case 'm':
	case 'M':
		estado.som_music = !estado.som_music;
		break;
	case 'i':
	case 'I':
		initEstado();
		initModelo();
		glutPostRedisplay();
		break;
	case 'f':
	case 'F':
		estado.fog.visivel = !estado.fog.visivel;
		break;
	}
}

void Special(int key, int x, int y) {

	switch (key) {
	case GLUT_KEY_F1: estado.teclas.cam1 = GL_TRUE;
		break;
	case GLUT_KEY_F2: estado.teclas.cam2 = GL_TRUE;
		break;
	case GLUT_KEY_F3: estado.teclas.cam3 = GL_TRUE;
		break;
	case GLUT_KEY_F6:
		break;
	case GLUT_KEY_UP: estado.teclas.up = GL_TRUE;
		break;
	case GLUT_KEY_DOWN: estado.teclas.down = GL_TRUE;
		break;
	case GLUT_KEY_LEFT: estado.teclas.left = GL_TRUE;
		break;
	case GLUT_KEY_RIGHT: estado.teclas.right = GL_TRUE;
		break;
	case GLUT_KEY_PAGE_UP:
		estado.fog.visivel = GL_TRUE;
		estado.fog.densidade = estado.fog.densidade + 0.0025;
		break;
	case GLUT_KEY_PAGE_DOWN:
		if (estado.fog.densidade - 0.0025 > 0) {
			estado.fog.densidade = estado.fog.densidade - 0.0025;
		}
		else {
			estado.fog.visivel = GL_FALSE;
		}
		break;
	}
}

void SpecialKeyUp(int key, int x, int y)
{
	switch (key)
	{
	case GLUT_KEY_UP: estado.teclas.up = GL_FALSE;
		break;
	case GLUT_KEY_DOWN: estado.teclas.down = GL_FALSE;
		break;
	case GLUT_KEY_LEFT: estado.teclas.left = GL_FALSE;
		break;
	case GLUT_KEY_RIGHT: estado.teclas.right = GL_FALSE;
		break;
	case GLUT_KEY_F1: estado.teclas.cam1 = GL_FALSE;
		break;
	case GLUT_KEY_F2: estado.teclas.cam2 = GL_FALSE;
		break;
	case GLUT_KEY_F3: estado.teclas.cam3 = GL_FALSE;
		break;
	}
}

void setProjection(int x, int y, GLboolean picking) {
	glLoadIdentity();
	if (picking)
	{ // se está no modo picking, lê viewport e define zona de picking
		GLint vport[4];
		glGetIntegerv(GL_VIEWPORT, vport);
		gluPickMatrix(x, glutGet(GLUT_WINDOW_HEIGHT) - y, 4, 4, vport); // Inverte o y do rato para corresponder à jana
	}

	gluPerspective(estado.camera.fov, (GLfloat)glutGet(GLUT_WINDOW_WIDTH) / glutGet(GLUT_WINDOW_HEIGHT), 1, modelo.dimensao * 2.5);
}

void myReshape(int w, int h) {
	glViewport(0, 0, w, h);
	glMatrixMode(GL_PROJECTION);
	setProjection(0, 0, GL_FALSE);
	glMatrixMode(GL_MODELVIEW);
}

void motionRotate(int x, int y)
{
#define DRAG_SCALE	0.01
	double lim = M_PI / 2 - 0.1;
	estado.camera.dir_long -= (estado.xMouse - x)*DRAG_SCALE;
	estado.camera.dir_lat += (estado.yMouse - y)*DRAG_SCALE*0.5;
	if (estado.camera.dir_lat > lim)
		estado.camera.dir_lat = lim;
	else
		if (estado.camera.dir_lat < -lim)
			estado.camera.dir_lat = -lim;
	estado.xMouse = x;
	estado.yMouse = y;
	glutPostRedisplay();
}


int picking(int x, int y) {
	int i, n, objid = 0;
	double zmin = 10.0;
	GLuint buffer[100], *ptr;

	glSelectBuffer(100, buffer);
	glRenderMode(GL_SELECT);
	glInitNames();

	glMatrixMode(GL_PROJECTION);
	glPushMatrix(); // guarda a projecção
	glLoadIdentity();
	setProjection(x, y, GL_TRUE);

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	setNavigateWindowCamera(&estado.camera, modelo.objecto);
	//desenhaEixos();
	desenhaLabirinto();
	//initModelos3D();

	n = glRenderMode(GL_RENDER);
	if (n > 0)
	{
		ptr = buffer;
		for (i = 0; i < n; i++)
		{
			if (zmin > (double) ptr[1] / UINT_MAX) {
				zmin = (double)ptr[1] / UINT_MAX;
				objid = ptr[3];
			}
			ptr += 3 + ptr[0]; // ptr[0] contem o número de nomes (normalmente 1); 3 corresponde a numnomes, zmin e zmax
		}
	}


	glMatrixMode(GL_PROJECTION); //repõe matriz projecção
	glPopMatrix();
	glMatrixMode(GL_MODELVIEW);

	return objid;
}
void mouse(int btn, int state, int x, int y)
{
	switch (btn)
	{
	case GLUT_RIGHT_BUTTON:
		if (state == GLUT_DOWN)
		{
			estado.xMouse = x;
			estado.yMouse = y;
			//if (glutGetModifiers() & GLUT_ACTIVE_CTRL)
			//	glutMotionFunc(motionZoom);
			//else
			glutMotionFunc(motionRotate);
			cout << "Left down\n";
		}
		else
		{
			glutMotionFunc(NULL);
			cout << "Left up\n";
		}
		break;
	case GLUT_LEFT_BUTTON:
		if (state == GLUT_DOWN)
		{
			//estado.eixoTranslaccao = picking(x, y);

			//estado.nodeIdentity = picking(x, y);
			estado.wayIdentity = picking(x, y);
			//estado.poiIdentity = picking(x, y);

			//if (estado.eixoTranslaccao)
			//	glutMotionFunc(motionDrag);
			//cout << "Right down - objecto:" << estado.eixoTranslaccao << endl;

			if (estado.nodeIdentity)
				cout << "No selecionado: nos[" << estado.nodeIdentity << "]" << endl;

			if (estado.wayIdentity)
				cout << "Caminho selecionado: way[" << estado.wayIdentity << "]" << endl;

			//if (estado.poiIdentity)
			//	cout << "POI selecionado: " << estado.poiIdentity << endl;
		}
		else
		{
			if (estado.nodeIdentity != 0)
				estado.nodeIdentity = 0;

			if (estado.wayIdentity != 0)
				estado.wayIdentity = 0;

			//if (estado.eixoTranslaccao != 0)
			//{
			//	estado.camera.center[0] = estado.eixo[0];
			//	estado.camera.center[1] = estado.eixo[1];
			//	estado.camera.center[2] = estado.eixo[2];
			//	glutMotionFunc(NULL);
			//	estado.eixoTranslaccao = 0;
			//	glutPostRedisplay();
			//}

			glutMotionFunc(NULL);
			cout << "Right up\n";
		}
		break;
	}
}


void myInit()
{
	GLfloat LuzAmbiente[] = { 0.3, 0.3, 0.3, 0.0 };
	glClearColor(0.0, 0.0, 0.0, 0.0);

	glGenTextures(NUM_TEXTURES, modelo.g_Textures);
	LoadAllTextures();
	glEnable(GL_TEXTURE_2D);

	if (guestMode)
		LoadXmlFile(); // carrega dados dos nós e arcos do ficheiro xml
	else
	{
		//rest.getPois();
		//rest.geVisitsList();
		//restClient rest = restClient(WEBAPI);
		//LoadFromAPI(rest, modelo.objecto.pos.x-RAIO_ACCAO, modelo.objecto.pos.y - RAIO_ACCAO, modelo.objecto.pos.x+RAIO_ACCAO, modelo.objecto.pos.y+RAIO_ACCAO, true);
	}



	g_FontListID = CreateOpenGL3DFont("Impact", FONT_EXTRUDE);

	initModelo();
	initEstado();
	initModelos3D();
	CalculateModelVariables();
	LoadRoute();

	modelo.quad = gluNewQuadric();
	gluQuadricDrawStyle(modelo.quad, GLU_FILL);
	gluQuadricNormals(modelo.quad, GLU_OUTSIDE);

	glEnable(GL_SMOOTH); /*enable smooth shading */
	glEnable(GL_LIGHTING); /* enable lighting */
	glEnable(GL_DEPTH_TEST); /* enable z buffer */

	glEnable(GL_NORMALIZE);
	//glEnable(GL_COLOR_MATERIAL);
	glDepthFunc(GL_LESS);

	glLightModelfv(GL_LIGHT_MODEL_AMBIENT, LuzAmbiente);
	glLightModeli(GL_LIGHT_MODEL_LOCAL_VIEWER, estado.lightViewer);
	glLightModeli(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE);
}
void LoadHeaderMenu()
{
	system("cls");
	cout << endl;
	cout << "\t\t__________________________________________________" << endl;
	cout << "\t\t            LAPR5 - Porto 3D simulator" << endl;
	cout << "\t\t__________________________________________________" << endl;
	cout << endl;
}

bool LoadLoginMenu(restClient *rest)
{
	LoadHeaderMenu();
	string tempUsr;
	string tempPsw;
	cout << "\t\tSign-in:" << endl;
	cout << "\t\tUsername: "; cin >> tempUsr;
	cout << "\t\tPassword: "; cin >> tempPsw;

	rest->setCredentials(tempUsr, tempPsw); // Credenciais de tentativa de sessao
	// limpa temporarias
	tempUsr = "";
	tempPsw = "";

	if (rest->login()) {
		guestMode = false;
		return true;
	}
	else {
		guestMode = true;
		return false;
	}
}

bool scheduledVisits(restClient *rest)
{
	LoadHeaderMenu();
	int resp;
	vector<int> routePOIs;

	cout << "\t\tScheduled visits: " << endl;
	if (rest->getVisitsList())
	{
		cout << "\t\tSelect visit id: "; cin >> resp;
		rest->setVisitId(resp);
		routePOIs = rest->getRoutePOIS();
	}
	else
		return false;
}

void main(int argc, char **argv)
{
	if (!LoadLoginMenu(&rest))
	{
		cout << endl << "\t\tThe user name or password is incorrect." << endl << "\t\t";
		cout << endl << "\t\tEntering guest mode..." << endl << "\t\t";
		system("pause");
	}
	else {
		cout << endl << "\t\tWelcome " << rest.getUser() << " !!!" << endl;
		if (!scheduledVisits(&rest))
		{
			cout << "\t\tVisit not found, try again..." << endl << "\t\t";
			system("pause");
			scheduledVisits(&rest);
		}
	}

	glutInit(&argc, argv);
	alutInit(&argc, argv);

	InitAudio();
	InitAudioMusic();


	/* need both double buffering and z buffer */

	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB | GLUT_DEPTH);
	glutInitWindowSize(800, 600);
	glutCreateWindow("Porto3D");
	glutReshapeFunc(myReshape);

	myInit();
	imprime_ajuda();

	glutDisplayFunc(display);
	glutTimerFunc(estado.timer, Timer, 0);
	glutKeyboardFunc(keyboard);
	glutSpecialFunc(Special);
	glutSpecialUpFunc(SpecialKeyUp);
	glutMouseFunc(mouse);
	audio("audio//icantwait.wav");

	glutMainLoop();
}
