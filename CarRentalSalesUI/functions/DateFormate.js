
export function formattedDate(dateString) {
  const date = new Date(dateString);


  if (isNaN(date.getTime())) {
    return "Invalid Date";
  }

  return date.toLocaleString('en-US', {
    weekday: 'short',
    year: 'numeric',
    month: '2-digit',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
    hour12: true
  });
}