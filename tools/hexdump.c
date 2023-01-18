#include <ctype.h>
#include <errno.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main(int argc, char **argv) {
	if (argc != 2) {
		fprintf(stderr, "%s: usage %s FILE\n",
			*argv,
			*argv);
		return 1;
	}
	FILE *fp = fopen(argv[1], "rb");
	if (!fp) {
		fprintf(stderr, "%s: error when opening file: %s\n",
			*argv,
			strerror(errno));
		return 2;
	}
	uint8_t buffer[16];
	uint8_t *buf = buffer;
	size_t i, j, k;
	while ((j = fread(buf, 1, 16, fp)) > 0) {
		printf("%016lX: ", ftell(fp) - j);
		for (i = 0; i < j; ++i) {
			printf("%02hhX ", buf[i]);
		}
		k = 16 - j;
		for (i = 0; i < k; ++i) {
			printf("   ");
		}
		printf("| '");
		for (i = 0; i < j; ++i) {
			putchar(isprint(buf[i])
				? buf[i]
				: '.');
		}
		puts("'");
	}
	fclose(fp);
	return 0;
}
