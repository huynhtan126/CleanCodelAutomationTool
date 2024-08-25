from selenium import webdriver
import requests
from bs4 import BeautifulSoup
import os
driver = webdriver.Chrome()
driver.get('https://example.com')
soup = BeautifulSoup(driver.page_source, 'html.parser')
resources = soup.find_all(['img', 'link', 'script'])
for resource in resources:
    if 'src' in resource.attrs:
        url = resource['src']
    elif 'href' in resource.attrs:
        url = resource['href']
    else:
        continue
    
    response = requests.get(url)
    filename = os.path.basename(url)
    with open(filename, 'wb') as file:
        file.write(response.content)
driver.quit()
