﻿{
    var title;
    var description;
    var videoUri;

    // Here we define our query as a multi-line string
    // Storing it in a separate .graphql/.gql file is also possible
    var query = `
        query ($id: Int) { # Define which variables will be used in the query (id)
        Media (id: $id, type: ANIME) { # Insert our variables into the query arguments (id) (type: ANIME is hard-coded in the query)
            id
            title {
            romaji
            english
            native
            }
        }
    }
    `;

    // Define our query variables and values that will be used in the query request
    var variables = {
        id: 15125
    };

    // Define the config we'll need for our Api request
    var url = 'https://graphql.anilist.co';

    // Make the HTTP Api request
    var data = fetch(url, JSON.stringify({ query: query, variables: variables }));

    handleData(data);

    function handleData(data) {
        log(data);
        var json = JSON.parse(data);
        title = json.data.Media.title.english;
        description = "Hello World";
        videoUri = "https://streamani.net/goto.php?url=aHR0cHM6LyURASDGHUSRFSJGYfdsffsderFStewthsfSFtrftesdf9zdG9yYWdlLmdvb2dsZWFwaXMuY29tL3RvdGVtaWMtc2VjdG9yLTMxNzAwMi9ZRDREVkdWMVk0Wi9zdDIzX3ZpdnktZmx1b3JpdGUtZXllcy1zb25nLWVwaXNvZGUtMTEuMTYyNTkxMTQ4NS5tcDQ=".replace("https://", "http://");
    }
}