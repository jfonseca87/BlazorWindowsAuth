window.fetchOAuthData = async function () {
    try {
        const response = await fetch('http://localhost:5187/api/oauth', {
            method: 'GET',
            credentials: 'include'  // This tells the browser to include cookies/credentials
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        // Assuming the endpoint returns text; use response.json() if JSON is returned.
        const data = await response.text();
        return data;
    } catch (error) {
        console.error('Error fetching OAuth data:', error);
        return null;
    }
};