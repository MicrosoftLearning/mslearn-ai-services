import os
from azure.core.credentials import AzureKeyCredential
from azure.ai.contentsafety import ContentSafetyClient
from azure.ai.contentsafety.models import AnalyzeTextOptions
from dotenv import load_dotenv

# Load environment variables from .env file
load_dotenv()

# Replace with your actual endpoint and key
endpoint = os.getenv("CONTENT_SAFETY_ENDPOINT")
key = os.getenv("CONTENT_SAFETY_KEY")

client = ContentSafetyClient(endpoint, AzureKeyCredential(key))

# Sample text for analysis
text_to_analyze = "I want to hurt myself."
options = AnalyzeTextOptions(text=text_to_analyze)

response = client.analyze_text(options)

print("Categories:", response.categories_analysis)