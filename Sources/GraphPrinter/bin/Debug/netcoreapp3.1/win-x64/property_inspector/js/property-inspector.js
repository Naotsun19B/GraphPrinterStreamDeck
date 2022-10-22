// global websocket, used to communicate from/to Stream Deck software
// as well as some info about our plugin, as sent by Stream Deck software 
let websocket = null,
	uuid = null,
	inInfo = null,
	actionInfo = {},
	settingsModel = {
		ServerURL: "ws://127.0.0.1:3000/",
		ServerProtocol: "ws"
	};

function connectElgatoStreamDeckSocket(inPort, inUUID, inRegisterEvent, inInfo, inActionInfo) {
	uuid = inUUID;
	actionInfo = JSON.parse(inActionInfo);
	inInfo = JSON.parse(inInfo);
	websocket = new WebSocket('ws://localhost:' + inPort);

	//initialize values
	if (actionInfo.payload.settings.settingsModel) {
		settingsModel.ServerURL = actionInfo.payload.settings.settingsModel.ServerURL;
		settingsModel.ServerProtocol = actionInfo.payload.settings.settingsModel.ServerProtocol;
	}

	document.getElementById('txtServerURLValue').value = settingsModel.ServerURL;
	document.getElementById('txtServerProtocolValue').value = settingsModel.ServerProtocol;

	websocket.onopen = function () {
		const json = {event: inRegisterEvent, uuid: inUUID};
		// register property inspector to Stream Deck
		websocket.send(JSON.stringify(json));
	};

	websocket.onmessage = function (evt) {
		// Received message from Stream Deck
		const jsonObj = JSON.parse(evt.data);
		const sdEvent = jsonObj['event'];
		switch (sdEvent) {
			case "didReceiveSettings":
				if (jsonObj.payload.settings.settingsModel.ServerURL) {
					settingsModel.ServerURL = jsonObj.payload.settings.settingsModel.ServerURL;
					document.getElementById('txtServerURLValue').value = settingsModel.ServerURL;
				}
				if (jsonObj.payload.settings.settingsModel.ServerProtocol) {
					settingsModel.ServerProtocol = jsonObj.payload.settings.settingsModel.ServerProtocol;
					document.getElementById('txtServerProtocolValue').value = settingsModel.ServerProtocol;
				}
				break;
			default:
				break;
		}
	};
}

const setSettings = (value, param) => {
	if (websocket) {
		settingsModel[param] = value;
		const json = {
			"event": "setSettings",
			"context": uuid,
			"payload": {
				"settingsModel": settingsModel,
				settings: undefined
			}
		};
		websocket.send(JSON.stringify(json));
	}
};

