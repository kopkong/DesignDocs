//
//  utility.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-19.
//
//

#ifndef RealFightSimu_utility_h
#define RealFightSimu_utility_h

#include <string>
using namespace std;

inline void splitString(const string inputStr,char delim,string* outputArray,int arrayLength)
{
	string newStr = inputStr;
    
	int i = 0;
	int startPos = 0;
	string tmpStr;
    
	int delimPos = newStr.find(delim,startPos);
    
	while( delimPos != string::npos && i< arrayLength)
	{
		tmpStr = newStr.substr(startPos,delimPos - startPos);
		outputArray[i] = tmpStr.c_str();
        
		startPos = delimPos + 1;
		delimPos = newStr.find(delim,startPos);
		i++;
        
	}
}


// 将int数组组成一个String
inline void composeString(int* inputString,int arrayLength, string delim, string* outputString)
{
    int pos = 0;
    char tmp[64];
    
    while (pos < arrayLength) {
        sprintf(tmp,"%d",inputString[pos]);
        string s(tmp);
        *outputString+= (s + delim);
        ++pos;
    }
}

#endif
