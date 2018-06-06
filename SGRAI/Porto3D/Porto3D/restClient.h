#pragma once

using namespace std;

#include <string>
#include <curl.h>

class restClient
{

private:
	string username;
	string password;
	string accesstoken;
	int visitId;

public:
	string server;

	restClient();
	restClient(string server);
	~restClient();

	void setCredentials(string usr, string psw);
	string getUser();
	void setVisitId(int id);
	bool getVisitsList();
	vector<int> getRoutePOIS();
	bool getPoiInfo(int poiId);
	bool login();

	static size_t WriteCallback(void *contents, size_t size, size_t nmemb, void *userp);
};
