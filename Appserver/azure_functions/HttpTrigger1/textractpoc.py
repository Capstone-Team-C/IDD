import boto3
import logging
from . import config

req_dict = {}
req_dict['FeatureTypes'] = ['TABLES', 'FORMS']

# Doc is either an azure functions HttpRequest
# get_json() or get_body() response. Rt is the
# request type: json | bytes
def submission_to_aws(doc, rt):
    logging.info("AWS req from " + rt)
    # Convert document to byte array
    img_string = None
    try:
        img_string = bytearray(doc)
    except Exception as e:
        return "Document error " + str(e)

    keys = config.get_keys()
    # Instantiate AWS Client 
    client = boto3.client(
        'textract',
        aws_access_key_id=keys['AWS_KEY'],
        aws_secret_access_key=keys['AWS_SECRET'],
        region_name=keys['AWS_REGION'])

    # Send document for analysis
    response = client.analyze_document(
        Document={'Bytes': bytes(img_string)},
        FeatureTypes=['TABLES', 'FORMS']
    )

    return response


# Does the same as the above method without
# converting to a bytearray.
def submission_to_awsBytes(doc, rt):
    logging.info("AWS req from " + rt)

    keys = config.get_keys()
    # Instantiate AWS Client 
    client = boto3.client(
        'textract',
        aws_access_key_id=keys['AWS_KEY'],
        aws_secret_access_key=keys['AWS_SECRET'],
        region_name=keys['AWS_REGION'])

    # Send document for analysis
    response = client.analyze_document(
        Document={'Bytes': bytes(doc)},
        FeatureTypes=['TABLES', 'FORMS']
    )

    return response


