# Solution for Problem faced while deploying

- **Connection String:**
	 - data source={ServerName}; Initial Catalog={DatabaseName}; integrated security=False;User Id={UserName};Password={**********} 
 - **Web.Config:**
   - Need to remove all from <system.codedom></system.codedom>
   - Need to add inside <system.web></system.web>
 ```html
	 <customErrors mode="Off"/>
	 <trust level="Full"/>
  ```
