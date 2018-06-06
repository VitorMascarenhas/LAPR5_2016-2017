#pragma once

#include <iostream>
#include <vector>
#include <stdlib.h>
#include <json.h>
#include "restClient.h"

//#pragma comment(lib, "libcurl_imp.lib") /* Livraria do Curl*/

using namespace std;

restClient::restClient() { }
restClient::restClient(string srv) { server = srv; }
restClient::~restClient() { }

void restClient::setCredentials(string usr, string psw) {
	username = usr;
	password = psw;
}

string restClient::getUser() {
	return username;
}

void restClient::setVisitId(int id)
{
	visitId = id;
}

size_t restClient::WriteCallback(void *contents, size_t size, size_t nmemb, void *userp)
{
	((string*)userp)->append((char*)contents, size * nmemb);
	return size * nmemb;
}

bool restClient::getVisitsList()
{
	CURL *curl;
	CURLcode res;
	string readBuffer;

	curl_global_init(CURL_GLOBAL_ALL);
	curl = curl_easy_init();
	if (curl)
	{
		struct curl_slist *headers = NULL;
		headers = curl_slist_append(headers, "Accept: application/json");
		headers = curl_slist_append(headers, "Content-Type: application/json");
		headers = curl_slist_append(headers, "charset: UTF-8");

		if (accesstoken.length() > 1)
		{
			string bearerToken = "Authorization: Bearer " + accesstoken;
			headers = curl_slist_append(headers, bearerToken.c_str());
		}
		curl_easy_setopt(curl, CURLOPT_URL, (server + "/api/visit/user").c_str());
		curl_easy_setopt(curl, CURLOPT_HTTPGET, 1);
		curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
		curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
		curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);
		res = curl_easy_perform(curl);

		if (res != CURLE_OK)
			fprintf(stderr, "curl_easy_perform() failed: %s\n",
				curl_easy_strerror(res));
		else
		{
			Json::Value root;
			Json::Reader reader;
			bool succeeded = reader.parse(readBuffer, root);
			curl_easy_cleanup(curl);
			curl_global_cleanup();

			if (succeeded)
			{
				cout << "\t\tId\tName\t\t\tStart date\t\tEndDate" << endl;
				Json::Value root;
				istringstream iss(readBuffer);
				iss >> root;
				for (Json::Value::iterator it = root.begin(); it != root.end(); it++)
				{
					Json::Value visitasJson = (*it);
					int id = visitasJson["Id"].asInt();
					string name = visitasJson["Name"].asString();
					string startDate = visitasJson["StartDate"].asString();
					string endDate = visitasJson["Enddate"].asString();
					cout << "\t\t" << id << "\t" << name << "\t\t\t" << startDate << "\t\t" << endDate << endl;
				}
				return true;
			}
			else
				return false;
		}
	}
	curl_global_cleanup();
	return false;
}

//string restClient::getModelPath(string desc)
//{
//	string text = "Casa da Musica";
//	if (desc.compare(text) == 0)
//		return "Models/casa_da_musica/casa_da_musica.3ds";
//
//	text = "Estadio do Dragao";
//	if (desc.compare(text) == 0)
//		return "Models/estadio_dragao/estadio_dragao.3ds";
//
//	text = "Torre dos Clerigos";
//	if (desc.compare(text) == 0)
//		return "Models/torre_clerigos/torre_clerigos.3ds";
//
//	text = "ISEP";
//	if (desc.compare(text) == 0)
//		return "Models/ISEP/isep.3ds";
//
//	else
//		return "False";
//}
//
//float restClient::getModelScale(string desc)
//{
//	float scale;
//	string text = "Casa da Musica";
//	if (desc.compare(text) == 0)
//		return 1.0;
//
//	text = "Estadio do Dragao";
//	if (desc.compare(text) == 0)
//		return 0.5;
//
//	text = "Torre dos Clerigos";
//	if (desc.compare(text) == 0)
//		return 1.0;
//
//	text = "ISEP";
//	if (desc.compare(text) == 0)
//		return 1.0;
//	else
//		return -1;
//}

//bool LoadRest3dModels(int poiId, string poiDesc, double poiLat, double poiLong, double poiAlt)
//{
//	string path = getModelPath(poiDesc);
//	if (path.compare("False") != 0)
//		float scale = getModelScale(poiDesc);
//
//	modelo.poi[numPOIs].id = 1;
//	modelo.poi[numPOIs].path = "Models/casa_da_musica/casa_da_musica.3ds";
//	modelo.poi[numPOIs].name = "Casa da Musica";
//	modelo.poi[numPOIs].desc = "Casa da musica";
//	modelo.poi[numPOIs].x = 3900.0;
//	modelo.poi[numPOIs].y = -10.0;
//	modelo.poi[numPOIs].z = 0;
//	modelo.poi[numPOIs].scale = 1.0;
//	numPOIs++;
//}

vector<int> restClient::getRoutePOIS()
{
	CURL *curl;
	CURLcode res;
	string readBuffer;
	vector<int> routePois;

	curl_global_init(CURL_GLOBAL_ALL);
	curl = curl_easy_init();
	if (curl)
	{
		struct curl_slist *headers = NULL;
		headers = curl_slist_append(headers, "Accept: application/json");
		headers = curl_slist_append(headers, "Content-Type: application/json");
		headers = curl_slist_append(headers, "charset: UTF-8");

		if (accesstoken.length() > 1)
		{
			string bearerToken = "Authorization: Bearer " + accesstoken;
			headers = curl_slist_append(headers, bearerToken.c_str());
		}
		curl_easy_setopt(curl, CURLOPT_URL, (server + "/api/Route/visit/" + to_string(visitId)).c_str());
		curl_easy_setopt(curl, CURLOPT_HTTPGET, 1);
		curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
		curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
		curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);
		res = curl_easy_perform(curl);

		if (res != CURLE_OK)
			fprintf(stderr, "curl_easy_perform() failed: %s\n",
				curl_easy_strerror(res));
		else
		{
			Json::Value root;
			Json::Reader reader;
			bool succeeded = reader.parse(readBuffer, root);
			
			if (succeeded)
			{
				curl_easy_cleanup(curl);
				curl_global_cleanup();
				Json::Value root;
				istringstream iss(readBuffer);
				iss >> root;

				//cout << "Json Example pretty print: " << endl << root.toStyledString() << endl;

				for (Json::Value::iterator it = root.begin(); it != root.end(); it++)
				{
					Json::Value routeJson = (*it);
					// POIS
					Json::Value poiJson = routeJson["PointOfInterest"];
					int poiId = poiJson["Id"].asInt();
					//string poiDesc = poiJson["Description"].asString();
					//double poiLat = poiJson["Latitude"].asDouble();
					//double poiLong = poiJson["Longitude"].asDouble();
					//double poiAlt = poiJson["Altitude"].asDouble();
					//LoadRest3dModels(poiId, poiDesc, poiLat, poiLong, poiAlt);
					routePois.push_back(poiId);
				}
				return routePois;
			}
			else
				return routePois;
		}
	}
	curl_global_cleanup();
	return routePois;
}

bool restClient::getPoiInfo(int poiId)
{
	CURL *curl;
	CURLcode res;
	string readBuffer;

	curl_global_init(CURL_GLOBAL_ALL);
	curl = curl_easy_init();
	if (curl)
	{
		struct curl_slist *headers = NULL;
		headers = curl_slist_append(headers, "Accept: application/json");
		headers = curl_slist_append(headers, "Content-Type: application/json");
		headers = curl_slist_append(headers, "charset: UTF-8");

		if (accesstoken.length() > 1)
		{
			string bearerToken = "Authorization: Bearer " + accesstoken;
			headers = curl_slist_append(headers, bearerToken.c_str());
		}
		curl_easy_setopt(curl, CURLOPT_URL, (server + "/api/poi/" + to_string(poiId)).c_str());
		curl_easy_setopt(curl, CURLOPT_HTTPGET, 1);
		curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
		curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
		curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);
		res = curl_easy_perform(curl);


		if (res != CURLE_OK)
			fprintf(stderr, "curl_easy_perform() failed: %s\n",
				curl_easy_strerror(res));
		else
		{
			Json::Value root;
			Json::Reader reader;
			bool succeeded = reader.parse(readBuffer, root);

			if (succeeded)
			{
				curl_global_cleanup();
				return true;
			}
		}
	}
	curl_global_cleanup();
	return false;
}


bool restClient::login()
{
	CURL *curl;
	CURLcode res;
	string readBuffer;
	string login = "grant_type=password&username=" + username + "&password=" + password;

	curl_global_init(CURL_GLOBAL_ALL);
	curl = curl_easy_init();

	if (curl)
	{
		curl_easy_setopt(curl, CURLOPT_URL, (server + "/token").c_str());
		curl_easy_setopt(curl, CURLOPT_POSTFIELDS, login.c_str());
		curl_easy_setopt(curl, CURLOPT_POST, 1);
		curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
		curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);
		res = curl_easy_perform(curl);

		if (res != CURLE_OK)
			fprintf(stderr, "curl_easy_perform() failed: %s\n",
				curl_easy_strerror(res));
		else
		{
			Json::Value root;
			Json::Reader reader;
			bool succeeded = reader.parse(readBuffer, root);

			if (succeeded)
			{
				accesstoken = root.get("access_token", "").asString();
				curl_easy_cleanup(curl);
				curl_global_cleanup();

				if (accesstoken.length() > 1)
					return true;
				else
					return false;
			}
		}
	}
	curl_global_cleanup();
	return false;
}
