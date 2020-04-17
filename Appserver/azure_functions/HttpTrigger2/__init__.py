import logging
import json
import azure.functions as func
from . import textractpoc as POC


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger2 function processed a request.')

    # Do we have valid JSON in the body?
    try:
        req_body = req.get_json()
        aws_res = POC.submission_to_awsBytes(req_body, "json")
        ret = {"response_json": json.dumps(aws_res)}
        return func.HttpResponse(json.dumps(ret), mimetype="application/json")
    except Exception as e:
        logging.info(f"No JSON in body: {e}")

    # Do we have valid bytes in the body?
    try:
        req_body = req.get_body()
        aws_res = POC.submission_to_awsBytes(req_body, "bytes")
        ret = {"response_bytes": str(aws_res)}
        return func.HttpResponse(json.dumps(ret), mimetype="application/json")
    except Exception as e:
        logging.info(f"No Bytes in body: {e}")

    # Fail Case
    ret = {"response_miss": "Something doesn't smell right..."}
    return func.HttpResponse(json.dumps(ret), mimetype="application/json")
