#ifndef __RealFightSimu__UserData__
#define __RealFightSimu__UserData__

#include <iostream>
#include <map>
#include "json.h"

class UserData
{
private:


	Json* getRootJson(const char* szFileName);


protected:
	~UserData();

public:
	static UserData& Instance();
	void init();

	void setIntByKey();
	void setStringByKey();

	int getIntByKey(const char*);
	std::string getStringByKey(const char*);
};

#endif /* defined(__RealFightSimu__UserData__) */